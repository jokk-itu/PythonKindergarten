
# Minitwit on a docker swarm cluster using terraform

In this scenario we will create a scalable docker swarm cluster using terraform.

With terraform we will first create a Digital Ocean droplet that will initialize the swarm cluster, making it the leader. We then create a number of droplets that join the swarm cluster as managers and the a number of droplets that join the swarm cluster as workers. We also attach a public floating ip to the leader droplet, we store all of the terraform state in a Digital Ocean Space, so that it is avialble from any system that we gice access to modify our cluster, such as a CI pipeline.

When our swarm cluster is up and running we deploy minitwit to the cluster using a declarative docker stack file.

![](images/diagram.png)

## Setup

### Install jq

`jq` is a cli tool for parsing JSON. Some of the scripts use it.

On ubuntu install with:

```bash
sudo apt update && sudo apt install -y jq
```

On other systems:

Download the binaries here: https://stedolan.github.io/jq/ and put them in a folder included in your system's PATH.

### Install terraform

Follow the instructions here: https://learn.hashicorp.com/tutorials/terraform/install-cli

### Clone this repository


```bash
git clone https://github.com/itu-devops/itu-minitwit-docker-swarm-teraform
```

### Generate ssh key

Make sure to call this command from the root of the repository you just cloned.

```bash
mkdir ssh_key && ssh-keygen -t rsa -b 4096 -q -N '' -f ./ssh_key/terraform
```

### Setup secrets environment variables file

By default the script will look for a file called `secrets` in root of the repository, that contains the variables needed to interact with Digital Ocean.

Copy the template file:

```bash
cp secrets_template secrets
```

We need to fille in the blanks for the five environment variables:


```bash
export TF_VAR_do_token=
export SPACE_NAME=
export STATE_FILE=
export AWS_ACCESS_KEY_ID=
export AWS_SECRET_ACCESS_KEY=
```

The following steps outline how to get each variable:

#### Digital Ocean token

To be abel to provision digital ocean resources using terraform we need an API token. Log in to the cloud portal: https://cloud.digitalocean.com
Then navigate to the API page in left menu on the left: (https://cloud.digitalocean.com/account/api/).

From here click on `Generate New Token`. Enter a name for the token, like 'terraform'. After confirming the token will show in the list and can be copied. Note that this is the only time the token will be visible, and you'll have to regenerate the token if you need to see it again.

![](images/token.png)

The token string will be the value for the `TF_VAR_do_token` environment variable.

By adding the prefix `TF_VAR_` to the environment variable, terraform will load `TF_VAR_<variable>` as `<variable>`, thus we can conveiently load the secret without keeping it in any version controlled files. You could also not set the variable and provide it on the command line when prompted, when executing any of the terraform commands.

#### Digital Ocean Space

Digital Ocean's blob storage is called `Spaces`, they are essentially AWS s3 buckets but on Digital Ocean, and use an identical API, so tools designed for using s3 buckets will work with Spaces. We will use a 'Space' to store our terraform state file, more on that in a minute.

To create a new space click on the spaces tab in menu on the left (https://cloud.digitalocean.com/spaces), then click on the green dropdown `Create` and select 'Spaces.'

![](images/create_space.png)

Choose the Frankfurt datacenter region and enter a name for the space. I called my space 'minitwit-terraform', you must choose a different unique name.

![](images/create_space2.png)

Click 'Create Space'.

The name we entereted for the space will be the value for the `SPACE_NAME` value in the `secrets` file.

#### Spaces access keys

Next we need to generate a key pair to access our space. Go to the API page where we generated the API token. Click `Generate New Key`, and enter a name, like 'minitwit'. The key consits of two strings the key itself and the secret key. The keys will be displayed right after creation, and then never again, like with the API token, so make sure to save them. We put the key and secret key into the `secrets` file: the key is the value for `AWS_ACCESS_KEY_ID` and the secret key is the value for `AWS_SECRET_ACCESS_KEY`. The reason that the environment variable are called 'AWS...' is that the tools that utilitize them were made for interacting with AWS s3 buckets, but we can use them for Digital Ocean spaces as they share the same API, though the naming can get a little confusing.

![](images/access_keys.png)

#### Terraform state file

The final environment variable in the `secrets` file is the name of the terraform state file, by convention we call it `<project>/terraform.tfstate`, so we will use `minitwit/terraform.tfstate`, though you can call it what you want as long as it has a .tfstate filetype.

##### Finished secrets file

After the previous steps the `secrets` file looks like this:

```bash
export TF_VAR_do_token=59edc3c820e2470e5d672325b5bc842ef57de683652e1497f35f36f6c40a76d3
export SPACE_NAME=minitwit-terraform
export STATE_FILE=minitwit/terraform.tfstate
export AWS_ACCESS_KEY_ID=7LIYBLABEBMMMQP66E7K
export AWS_SECRET_ACCESS_KEY=yp2l53hk1odj1F2FOdd64H4plieEPrn3+ke1Y53NpuE
```

(these are just example values and were destroyed after writing this guide.)

We now have all we need to bootsrap the docker swarm cluster and run minitwit on it.



# Docker Stack

`docker stack` is `docker-compose` but for swarm clusters. docker stacks let's us declaratively configure our services that we want to run in our cluster. A docker stack file is the same as a docker-compose but has a few more keys available, namely `deploy`, that let's us specify the number of replicas for `replicated services` or specify a service as a `global` service (1 container on each node).

Relevant Docker documentation:
* https://docs.docker.com/compose/swarm/
* https://docs.docker.com/compose/compose-file/#deploy

The minitwit stack:

```yaml
version: '3.3'

volumes:
  mysql_data:

services:

  visualizer:
    image: dockersamples/visualizer:stable
    ports:
      - "8888:8080"
    volumes:
      - "/var/run/docker.sock:/var/run/docker.sock"
    deploy:
      placement:
        constraints: [node.role == manager]

  loadbalancer:
    image: nginx
    ports:
      - '80:80'
    volumes:
      - /loadbalancer:/etc/nginx/conf.d
    deploy:
      replicas: 2

  minitwitimage:
    image: devopsitu/minitwitimage
    ports:
      - '8080:5000'
    deploy:
      replicas: 10

  itusqlimage:
    image: devopsitu/itusqlimage
    ports:
      - '3306:3306'
    environment:
      - MYSQL_ROOT_PASSWORD=megetsikkertkodeord
    volumes:
      - mysql_data:/var/lib/mysql
    deploy:
      replicas: 1
```


## Loadbalancing

A docker swarm cluster will automatically route requests to containers in the cluster, such that you can make a request to any node in the cluster with the appropriate port, and you will be served from one of the replicas of that service in the cluster, even if there are no containers running on the node you are sending requests to. Thus there isn't an explicit need for external loadbalancers, though they might be convenient for managing certificates and other proxy rules. Thus we run a replicated nginx loadbalancer in the cluster for this scenario in order to demonstrate how one could do it, and also that it creates some complications, namely that this configuration requires us to bindmount the nginx configutarion file into the containers. This means that we must ensure that the configuration files are present on all nodes, as we do not know on which node the nginx containers will be created.

A good exercise could be to automate creating a nginx image that has the configuration inside the image as part of the bootstrapping and use that instead.


## Cluster databases and docker networks

In this configuration we declare one mysql database container to run in the cluster. For production setups the consensus in the industry seems to be to either run the databases outside the cluster, or use the cloud-provider's scalable database offerings. For the purposes of this scenario we run it in the cluster to first; highlight why this can be problematic, since we do not know which node the database will be created on, and thus where the state will be stored, further we cannot simply add more database containers as this will create race conditions where the different containers will have different state. The second thing worth noting is that what we do get from running in the cluster is trivial networking and loadbalancing. ALl of the minitwit app containers simply point to the database service name as the database hostname, we can do this because docker networks use the service names for DNS. This means that we can create trivial connections within the cluster, not just for databases, as long as it doesn't matter which container replica we reach.

You can inspect the networks of the docker swarm by SSH'ing to one of the nodes and using the `docker network` command.

![](images/docker_network.png)


## Docker swarm cluster roles

Nodes in docker swarm clusters have one of three roles:
* `leader` the primary manager, does the actual orchestration of the cluster
* `manager` nodes that can manage the cluster, commands can be issued to managers, and will be carried out by the leader. If leader becomes unavailable, a manager will be promoted to the new leader.
* `worker` hosts containers

**Do note that in a docker swarm cluster, the leader is also a manager, and all managers are also workers! (at least by default)**





# Bootstrap the swarm cluster

Run the bootstrap script:

```bash
bash bootstrap.sh
```

The script will do the following:
* load environment variables from the `secrets` file
* verify that all environment variables are set
* initialize terraform with spaces bucket backend
* validate the terraform configuration
* create the infrastructure
    * create a leader docker swarm node that initializes the swarm cluster
    * a number of manager nodes that join the swarm cluster
    * a number of worker nodes that join the swarm cluster
* run a script that will generate a configuration file for using nginx as a loadbalancer in swarm cluster
* upload the configuration file to each node in the cluster
    * this is required because we bindmount the configuration file into the loadbalancer container, and since we do not know what node it will run on, we have to have the file available on all nodes.
* deploy the minitwit stack to the cluster
* finally print the public ip-address attached to the cluster

![](images/bootstrap.png)

We can inspect the running cluster and stack with the visualizer container:

![](images/swarm_visualizer.png)





# Scaling the cluster and stack

We have a few options in terms of scaling the deployment:
* We can scale the deployment vertically by increasing the size of the droplet vms the cluster is running on, by editing the terraform file `minitwit_swarm_cluster.tf` and increasing the `size` key in the droplet resources. This would allow us to scale the minitwit stack by adding more containers for each cluster node.
* We can scale the deployment horizontally by increaing the number of nodes in the cluster, by changing the number of `count` key in terraform file.
* Finally we can scale the minitwit stack horizontally by increasing the number of replicas of each service in the stack. We do this by changing the integer value of the `replicas` key in the stack file `stack/minitwit_stack.yml` and uploading it to the swarm leader, and updating the running stack to the new desired state.

## Let's scale the cluster up

By default the cluster will initialze with one leader, 2 managers and 3 workers, so 6 total nodes. Let's increase that.

Edit the `minitwit_swarm_cluster.tf` file with your favourite text editor. Then change the worker count from 3 to 5 (line 124).

Source the secrets file `source secrets`, in order to load the `do_token` variable into your shell, you can also simply paste it when prompted in the next command.

Apply the new desired state to infrastructure by typing `terraform apply` and answering `yes` when prompted.

This should now add to new droplets and join them to the swarm cluster as workers.

Now ssh to the leader node:
```bash
ssh root@$(terraform output -raw minitwit-swarm-leader-ip-address) -i ssh_key/terraform
```
Verify that the two new workers are present
```bash
docker node ls
```

Verify that the stack is deployed and running smoothly
```bash
docker stack ps minitwit
```

Now exit the ssh session and edit the docker stack file on your local machine, the file is located in `stack/minitwit_stack.yml`. Change the replicas of the `minitwitimage` service from 10 to 15 (line 32). Save the file.

Scp the stack file to the leader node:
```bash
scp -i ssh_key/terraform stack/minitwit_stack.yml root@$(terraform output -raw minitwit-swarm-leader-ip-address):~
```

Now ssh to the leader node again.

Apply the new stack file to update the desired cluster state
```bash
docker stack up minitwit -c minitwit_stack.yml
```

Verify that the service has been updated from 10 to 15 replicas
```bash
docker service ls
```

Now feel free to play around with the system :-)


## Some commands for Interacting with the Minitwit stack

To interact with the swarm cluster and the minitwit stack we need to have shell on a node in the cluster, so we SSH to the leader node:
```bash
ssh root@$(terraform output -raw minitwit-swarm-leader-ip-address) -i ssh_key/terraform
```
### A few interesting docker swarm commands:

List all nodes
```bash
docker node ls
```
List containers on each node
```bash
for node in $(docker node ls -q); do docker node ps $node; done
```
![](images/nodes_ps.png)

List all services
```bash
docker service ls
```

List containers in a service
```bash
docker service ps <service-name>
```

![](images/service_ls.png)

You can also simply list all containers of a stack
```bash
docker stack ps <stack-name>
```

If we want to scale one of the services, we change the number of replicas in the stack file, in this case `minitwit_stack.yml` and then deploy it again. Docker will then modify the existing cluster state to match the new desired state, creating and removing services, and scaling existing ones.
```bash
docker stack deploy <stack-name> -c <stack-file>
```

![](images/stack_deploy.png)

We can manually scale a service
```bash
docker service scale <service-name>=<replicas>
```

We can use a for loop to redistribute containers across the cluster, though this will redeploy all containers.
Useful when new nodes have joined the cluster
```bash
for service in $(docker service ls -q); do docker service update --force $service; done
```

We can perform a rolling update of service
```bash
docker service update --image <image-name>:<tag> <service-name>
```

We can rollback the last update of a service
```bash
docker service rollback <service-name>
```


## Interacting with the swarm cluster

To interact with swarm cluster you can edit the `minitwit_swarm_cluster.tf` file, to scale the cluster change the `count` variable to the desired number of nodes (vms) and then run `$ terraform apply` to modify the existing infrastructure. You may be prompted for the value of the `do_token`, if so, simply load the environment variables of the `secrets` file in to your shell using source, eg: `source secrets`.

### Using Terraform

All the following commands are contextual to the directory that contains the terraform files.

Initialize terraform for the current project
```bash
terraform init
```

Verify that the terraform files follow correct syntax
```bash
terraform validate
```

Preview the changes to be made at next apply
```bash
terraform plan
```

Apply changes
```bash
terraform apply
```

Destroy all of the infrastructure
```bash
terraform destroy
```

List all outputs
```bash
terraform output
```
Note: the output command is designed for human readable output, so everything will be surrounded with "quotes". If you want to use this command in scripts to feed the output into other commands, you should add the option output -raw to omit the quotes.











## Terraform backends

Terraform maintains a `.tfstate` file that contains the current state of your infrastructure. `backends` are the different ways that terraform can store this state file. By default terraform will use the `local` backend which is simply creating files locally, which is fine for testing. For production deployments we want to store the statefiles a safe place, so that we don't loose them, and so that the state is not tied to a single machine for teams working on the same infrastructure.

Therefore in this scenario we use Digital Ocean space to store our terraform state files, such that all terraform commands interact wit the remote state.

### Integrating with CI

Another usecase for remote terraform state is that we can make the state avilable to our CI systems. Since they are simply stored in a s3-API-compatible blob store, we can use s3 tools to interact with the files. An example of how to do this is found in the `docker/s3cmd` directory. `s3cmd` is a cli tool for interacting with s3 buckets. the `docker/s3cmd` directory contains a dockerfile that contains s3cmd ready-to-use, by simply providing the keys as environment variables. The script `docker/s3cmd/get_terraform_state.sh` shows how to use the docker image to get the terraform state files. Terraform state files are plain JSON so we can use tools like `jq` to easily parse the values we need, like the ip addresses of the nodes. Also note in the example below that I `source` the secrets file to load the variables into my shell.

![](images/s3cmd.png)

#!/bin/bash

echo -e "\n--> Bootstrapping Minitwit\n"

echo -e "\n--> Loading environment variables from secrets fi
  ip_address = digitalocean_floating_ip.public-ip.ip_address
  droplet_id = digitalocean_droplet.minitwit-swarm-leader.idle\n"
source secrets

echo -e "\n--> Checking that environment variables are set\n"
# check that all variables are set
[ -z "$TF_VAR_do_token" ] && echo "TF_VAR_do_token is not set" && exit
[ -z "$SPACE_NAME" ] && echo "SPACE_NAME is not set" && exit
[ -z "$STATE_FILE" ] && echo "STATE_FILE is not set" && exit
[ -z "$AWS_ACCESS_KEY_ID" ] && echo "AWS_ACCESS_KEY_ID is not set" && exit
[ -z "$AWS_SECRET_ACCESS_KEY" ] && echo "AWS_SECRET_ACCESS_KEY is not set" && exit

echo -e "\n--> Destroying environment\n"
bash scripts/do_destroy.sh

echo -e "\n--> Initializing terraform\n"
# initialize terraform
terraform init \
    -backend-config "bucket=$SPACE_NAME" \
    -backend-config "key=$STATE_FILE" \
    -backend-config "access_key=$AWS_ACCESS_KEY_ID" \
    -backend-config "secret_key=$AWS_SECRET_ACCESS_KEY"

# check that everything looks good
echo -e "\n--> Validating terraform configuration\n"
terraform validate

# create infrastructure
echo -e "\n--> Creating Infrastructure\n"
terraform apply -auto-approve

##DELETE IN THE FUTURE##
# generate loadbalancer configuration
echo -e "\n--> Generating loadbalancer in 10 seconds\n"
sleep 10
bash scripts/do_load_balancer.sh

# deploy the stack to the cluster
echo -e "\n--> Deploying the Minitwit stack to the cluster\n"
ssh \
    -o 'StrictHostKeyChecking no' \
    root@$(terraform output -raw minitwit-swarm-leader-ip-address) \
    -i ssh_key/terraform \
    'docker stack deploy minitwit -c minitwit_stack.yml'
    
# deploy kibana and elastic search to database server
echo -e '\n--> Deploying Kibana and ElasticSearch to DB server'
ssh \
    -o 'StrictHostKeyChecking no' \
    root@$(terraform output -raw minitwit-database-ip-address) \
    -i ssh_key/terraform \
    'docker compose -d database_compose.yml'
    
echo -e "\n--> Done bootstrapping Minitwit"
echo -e "--> The dbs will need a moment to initialize, this can take up to a couple of minutes..."
echo -e "--> Site will be avilable @ http://$(terraform output -raw public_ip)"
echo -e "--> You can check the status of swarm cluster @ http://$(terraform output -raw minitwit-swarm-leader-ip-address):8888"
echo -e "--> ssh to swarm leader with 'ssh root@\$(terraform output -raw minitwit-swarm-leader-ip-address) -i ssh_key/terraform'"
echo -e "--> To remove the infrastructure run: terraform destroy -auto-approve"
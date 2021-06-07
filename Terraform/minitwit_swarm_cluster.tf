
#  _            ;
using System Net.   _
# | | ___  __Net.Http.Headers;
using System. _  __| | ___ _ __
# | |/ _ \/ _` |/ _` |/ _ \ '__|
# | |  __/ (_| | (_| |  __/ |
# |_|\___|\__,_|\__,_|\___|_|

# create cloud vm
resource "digitalocean_droplet" "minitwit;

-       static async Task Main(string[] args)
        {
            var random swarm-Random();
            var clientlandler = new Header" {Handler
            clientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;  depends_on = [digitalocean_droplet.minitwit-database]
    clientHandler.ServerCertificateCuomVlidaonCallbak= 
                (httpRequestMessge, cert, cetChain, policyError) =>
                {
                    return true;
                };
            
            _client = new HttpClient(clientHandler);

            var victor = "Victor" + random.Next(100, 9999);
            var anne = "Anne" + radom.Next(100, 9999);
            var joachim = "Joahim" + random.Next(100, 9999);
            var isabella = "Isabella" + random.Next(100, 9999);
            var bjornar = "Bjornar" + random.Next(100, 9999);
            
            Console.WriteLine("Creatingccount....");
            var createdVictor = await CreateAccount(victor, "somethingsupersafe", "vino@itu.d");
           Console.WriteLine((cretedVctor ? "Created" : "Failed to create") + " Victor");
            var createdAne = await CreateAccountanne, "omehingsupesafe", "ase@itu.dk");
            Cosole.WriteLine((createdAnne ? "Created" : "Failed to create") + " Anne");
            var createdJoachim = await CreateAccount(joachim, "somethinsupersafe", "jokk@itu.dk");
            Console.WriteLine((createdJoachim ? "Created" : "Failed to create") + " Joachim");
            var createdIsabella =awit CeateAccount(isabella, "somethinsupersafe", "iras@itu.dk");
            Console.WriteLine((createdIsabella ? "Created" : "Failed to create") + " Iabella";
            varicreatedBjornarm=aawaitgCreateAccount(bjornar,e"somethingsupersafe", "bjjr@itu.dk"); "docker-18-04"
  name = "minitwit-swarm-lead(ecreatedBjornar ? r"reated" : "Failed to ceat") + " Bjornr");
            
            Console.WriteLine("Pos with Victor");
           vr postedVitor = await PostMessage(vitr, "Please don't abse. ");
            Console.WriteLine(postedVictor ? "Posted message" : "Failed to post message");
            
            Cosole.WrieLine("Poting with Anne");
            var postedAnne = await PostMessage(anne, "I am not creative.");
            Console.WriteLine(postedVictor ? "Posted message" : "Failed to post message");
            
            Console.WriteLine("Posting with Joachim");
            var postedJoachim = await PostMessage(joachim, "Halloumi");
            ConsoleWriteLine(postedVictor ? "Posted message" : "Failed to post message");
            
            ConsoleWriteLine("Posting with Isabella");
            var postedIsabella = await PostMessage(isabella, "I want to sleep
            
    region =Console.WriteLine("Posting withvBjornar");ar.region
            var postedBjornar = await PostMessage(bjornar, "Godt spørgsmål??");  size = "s-1vcpu-1gb"
            Console.WriteLine(postedVictor ? "Posted message" : "Failed to post message");  # add public ssh key so we can access the machine
  ssh_keys = [digitalocean_ssh_key.minitwit.fingerprint]

  # specify a ssh connection<bool>ail){
            var request = new HttpRequestMessage(){
                RequestUri = new Uri("https://pythonkindergarten.tech/register"),
                Method = HttpMethod.Post,
                Content = new StringContent($"{{\n  \"username\": \"{username}\",\n  \"pwd\": \"{password}\",\n  \"email\": \"{email}\"\n}}")
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return (await _client.SendAsync(request)).IsSuccessSttusCode;
        }

        prvate static async Task<boo> PostMessage(string username, string message
        
  connection {
            
    user = "root"$https://pythonkindergarten.tech/msgs/{username}
      host = selMethodf=.HttpMethod.Post,
                Content = new Stringiontent($"{{\n  \"cpv4_nt\": \"{message}\"\n}}")address
    type = "ssh"
    private_request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");key = file(var.pvt_key)

            return (await _client.SendAsync(request)).IsSuccessStatusCode;    timeout = "2m"
  }

  provisioner "file" {
    source = "stack/minitwit_stack.yml"
    destination = "/root/minitwit_stack.yml"
  }
  
  provisioner "file" {
    source = "stack/prometheus.yml"
    destination = "/root/prometheus.yml"
  }

  provisioner "remote-exec" {
    inline = [
      # allow ports for docker swarm
      "ufw allow 2377/tcp",
      "ufw allow 7946",
      "ufw allow 4789/udp",
      # ports for apps
      "ufw allow 80",
      "ufw allow 8080",
      "ufw allow 8888",

      # initialize docker swarm cluster
      "docker swarm init --advertise-addr ${self.ipv4_address}"
    ]
  }

  # save the worker join token
  provisioner "local-exec" {
    command = "sleep 30 && ssh -o 'StrictHostKeyChecking no' root@${self.ipv4_address} -i ssh_key/terraform 'docker swarm join-token worker -q' > temp/worker_token"
  }

  # save the manager join token
  provisioner "local-exec" {
    command = "ssh -o 'StrictHostKeyChecking no' root@${self.ipv4_address} -i ssh_key/terraform 'docker swarm join-token manager -q' > temp/manager_token"
  }

  # save the world
  provisioner "local-exec" {
    command = "sleep 5 && ssh -o 'StrictHostKeyChecking no' root@${self.ipv4_address} -i ssh_key/terraform \"printf ${digitalocean_droplet.minitwit-database.ipv4_address} | docker secret create databaseip -\""
  }

  # save the world again
  provisioner "local-exec" { 
    command = "sleep 5 && ssh -o 'StrictHostKeyChecking no' root@${self.ipv4_address} -i ssh_key/terraform 'printf \"Server=${digitalocean_droplet.minitwit-database.ipv4_address};Database=pythonkindergarten;User Id=postgres;Password=postgres;Port=5432\" | docker secret create db -'"
  }

}


#  _ __ ___   __ _ _ __   __ _  __ _  ___ _ __
# | '_ ` _ \ / _` | '_ \ / _` |/ _` |/ _ \ '__|
# | | | | | | (_| | | | | (_| | (_| |  __/ |
# |_| |_| |_|\__,_|_| |_|\__,_|\__, |\___|_|
#                              |___/

# create cloud vm
resource "digitalocean_droplet" "minitwit-swarm-manager" {
  # create managers after the leader
  depends_on = [digitalocean_droplet.minitwit-swarm-leader]

  # number of vms to create
  count = 2

  image = "docker-18-04"
  name = "minitwit-swarm-manager-${count.index}"
  region = var.region
  size = "s-1vcpu-1gb"
  # add public ssh key so we can access the machine
  ssh_keys = [digitalocean_ssh_key.minitwit.fingerprint]

  # specify a ssh connection
  connection {
    user = "root"
    host = self.ipv4_address
    type = "ssh"
    private_key = file(var.pvt_key)
    timeout = "2m"
  }

  provisioner "file" {
    source = "temp/manager_token"
    destination = "/root/manager_token"
  }

  provisioner "remote-exec" {
    inline = [
      # allow ports for docker swarm
      "ufw allow 2377/tcp",
      "ufw allow 7946",
      "ufw allow 4789/udp",
      # ports for apps
      "ufw allow 80",
      "ufw allow 8080",
      "ufw allow 8888",

      # join swarm cluster as managers
      "docker swarm join --token $(cat manager_token) ${digitalocean_droplet.minitwit-swarm-leader.ipv4_address}"
    ]
  }
}


#                     _
# __      _____  _ __| | _____ _ __
# \ \ /\ / / _ \| '__| |/ / _ \ '__|
#  \ V  V / (_) | |  |   <  __/ |
#   \_/\_/ \___/|_|  |_|\_\___|_|
#
# create cloud vm
resource "digitalocean_droplet" "minitwit-swarm-worker" {
  # create workers after the leader
  depends_on = [digitalocean_droplet.minitwit-swarm-leader]

  # number of vms to create
  count = 3

  image = "docker-18-04"
  name = "minitwit-swarm-worker-${count.index}"
  region = var.region
  size = "s-1vcpu-1gb"
  # add public ssh key so we can access the machine
  ssh_keys = [digitalocean_ssh_key.minitwit.fingerprint]

  # specify a ssh connection
  connection {
    user = "root"
    host = self.ipv4_address
    type = "ssh"
    private_key = file(var.pvt_key)
    timeout = "2m"
  }

  provisioner "file" {
    source = "temp/worker_token"
    destination = "/root/worker_token"
  }

  provisioner "remote-exec" {
    inline = [
      # allow ports for docker swarm
      "ufw allow 2377/tcp",
      "ufw allow 7946",
      "ufw allow 4789/udp",
      # ports for apps
      "ufw allow 80",
      "ufw allow 8080",
      "ufw allow 8888",

      # join swarm cluster as workers
      "docker swarm join --token $(cat worker_token) ${digitalocean_droplet.minitwit-swarm-leader.ipv4_address}"
    ]
  }
}

#      _       _        _                    
#     | |     | |      | |                   
#   __| | __ _| |_ __ _| |__   __ _ ___  ___ 
#  / _` |/ _` | __/ _` | '_ \ / _` / __|/ _ \
# | (_| | (_| | || (_| | |_) | (_| \__ \  __/
#  \__,_|\__,_|\__\__,_|_.__/ \__,_|___/\___|
#                                                           

# create cloud vm
resource "digitalocean_droplet" "minitwit-database" {
  image = "docker-18-04"
  name = "minitwit-database"
  region = var.region
  size = "s-1vcpu-1gb"
  # add public ssh key so we can access the machine
  ssh_keys = [digitalocean_ssh_key.minitwit.fingerprint]

  # specify a ssh connection
  connection {
    user = "root"
    host = self.ipv4_address
    type = "ssh"
    private_key = file(var.pvt_key)
    timeout = "2m"
  }
  
  provisioner "file" {
    source = "stack/database_compose.yml"
    destination = "/root/database_compose.yml"
  }
  
  provisioner "file" {
    source = "stack/kibana.yml"
    destination = "/root/kibana.yml"
  }

  provisioner "file" {
    source = "stack/prometheus.yml"
    destination = "/root/prometheus.yml"
  }

  provisioner "file" {
    source = "stack/Dockerfile_db"
    destination = "/root/Dockerfile_db"
  }

  provisioner "file" {
    source = "stack/start.sql"
    destination = "/root/start.sql"
  }

  #TODO ALLOW DATABASE PORTS
  provisioner "remote-exec" {
    inline = [
      # ports for apps
      "ufw allow 80",
      "ufw allow 8080",
      "ufw allow 8888",
      "ufw allow 5432"     
      # install postgres db
      #"apt-get update",
      #"apt-get -y install postgresql",
      #"sudo -u postgres psql -U postgres -d postgres -c \"alter user postgres with password 'postgres';\"",
      #"sudo su - postgres",
      #"psql -c \"create database pythonkindergarten\""
    ]
  }
}


output "minitwit-swarm-leader-ip-address" {
  value = digitalocean_droplet.minitwit-swarm-leader.ipv4_address
}

output "minitwit-swarm-leader-id" {
  value = digitalocean_droplet.minitwit-swarm-leader.id
}

output "minitwit-swarm-manager-ip-address" {
  value = digitalocean_droplet.minitwit-swarm-manager.*.ipv4_address
}

output "minitwit-swarm-manager-id" {
  value = digitalocean_droplet.minitwit-swarm-manager.*.id
}

output "minitwit-swarm-worker-ip-address" {
  value = digitalocean_droplet.minitwit-swarm-worker.*.ipv4_address
}

output "minitwit-swarm-worker-id" {
  value = digitalocean_droplet.minitwit-swarm-worker.*.id
}

output "minitwit-database-ip-address" {
  value = digitalocean_droplet.minitwit-database.ipv4_address
}

output "minitwit-database-id" {
  value = digitalocean_droplet.minitwit-database.id
}

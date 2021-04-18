Vagrant.configure("2") do |config|
  config.ssh.private_key_path = 'ssh_keys/do_ssh_key'
  config.vm.box = 'digital_ocean' 
  config.vm.box_url = "https://github.com/devopsgroup-io/vagrant-digitalocean/raw/master/box/digital_ocean.box"
  config.vm.synced_folder "remote_files", "/vagrant", type: "rsync"

  config.vm.define "production" do |production|
    config.vm.provider :digital_ocean do |provider|
      provider.ssh_key_name = 'pythonkindergarten'
      provider.token = ENV["PYTHONKINDERGARTEN_DO_TOKEN"]
      provider.region = 'fra1'
      provider.image = 'ubuntu-18-04-x64'
      provider.size = 's-2vcpu-2gb'
      provider.privatenetworking = true
      
    end
    
    production.vm.hostname = "pythonkindergarten-production-server"
    production.vm.provision "shell", inline: <<-SHELL
      echo "Configuring firewall"
      sudo ufw default deny incoming
      sudo ufw default allow outgoing
      sudo ufw allow ssh
      sudo ufw allow http
      sudo ufw allow https
      echo "Reloading firewall"
      sudo ufw reload

      echo "Installing Docker"
      curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo apt-key add -
      sudo add-apt-repository \
      "deb [arch=amd64] https://download.docker.com/linux/ubuntu \
      $(lsb_release -cs) \
      stable"
      sudo apt-get update
      yes | sudo apt install docker-ce docker-ce-cli containerd.io
      yes | sudo apt install docker-compose

      echo "+---------------------------------------------------------------+"
      echo "|         Finally DONE and ready for Travis Delployment         |"
      echo "+---------------------------------------------------------------+"
    SHELL
  end

  config.vm.define "slave" do |slave|
    config.vm.provider :digital_ocean do |provider|
      provider.ssh_key_name = 'pythonkindergarten'
      provider.token = ENV["PYTHONKINDERGARTEN_DO_TOKEN"]
      provider.region = 'fra1'
      provider.image = 'ubuntu-18-04-x64'
      provider.size = 's-2vcpu-2gb'
      provider.privatenetworking = true
      
    end
    
    slave.vm.hostname = "pythonkindergarten-production-slave"
    slave.vm.provision "shell", inline: <<-SHELL
      echo "Configuring firewall"
      sudo ufw default deny incoming
      sudo ufw default allow outgoing
      sudo ufw allow ssh
      sudo ufw allow http
      sudo ufw allow https
      echo "Reloading firewall"
      sudo ufw reload

      echo "Installing Docker"
      curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo apt-key add -
      sudo add-apt-repository \
      "deb [arch=amd64] https://download.docker.com/linux/ubuntu \
      $(lsb_release -cs) \
      stable"
      sudo apt-get update 
      yes | sudo apt install docker-ce docker-ce-cli containerd.io
      yes | sudo apt install docker-compose

      echo "+---------------------------------------------------------------+"
      echo "|                         Slave ready                           |"
      echo "+---------------------------------------------------------------+"
    SHELL
  end

  config.vm.define "database" do |database|
    config.vm.provider :digital_ocean do |provider|
      provider.ssh_key_name = 'pythonkindergarten'
      provider.token = ENV["PYTHONKINDERGARTEN_DO_TOKEN"]
      provider.region = 'fra1'
      provider.image = 'ubuntu-18-04-x64'
      provider.size = 's-2vcpu-4gb'
      provider.privatenetworking = true
      
    end
    
    database.vm.hostname = "pythonkindergarten-database-server"
    database.vm.provision "shell", inline: <<-SHELL
      sudo sh -c 'echo "deb http://apt.postgresql.org/pub/repos/apt $(lsb_release -cs)-pgdg main" > /etc/apt/sources.list.d/pgdg.list'
      wget --quiet -O - https://www.postgresql.org/media/keys/ACCC4CF8.asc | sudo apt-key add -
      sudo apt-get update
      sudo apt-get -y install postgresql
      sudo ufw allow 5432

      echo "+---------------------------------------------------------------+"
      echo "|             Finally DONE and ready for data                   |"
      echo "+---------------------------------------------------------------+"
    SHELL
  end
end

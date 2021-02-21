# -*- mode: ruby -*-
# vi: set ft=ruby :

# Since the webserver needs the IP of the DB server the two have to be started 
# in the right order and with storing the IP of the latter on the way:
#
# $ rm db_ip.txt | vagrant up | python store_ip.py

$ip_file = "db_ip.txt"

Vagrant.configure("2") do |config|
    config.vm.box = 'digital_ocean'
    config.vm.box_url = "https://github.com/devopsgroup-io/vagrant-digitalocean/raw/master/box/digital_ocean.box"
    config.ssh.private_key_path = '~/.ssh/id_rsa'
    config.vm.synced_folder ".", "/vagrant", type: "rsync"

    config.vm.define "kindergartenserver", primary: false do |server|
  
      server.vm.provider :digital_ocean do |provider|
        provider.ssh_key_name = ENV["SSH_KEY_NAME"]
        provider.token = ENV["DIGITAL_OCEAN_TOKEN"]
        provider.image = 'ubuntu-18-04-x64'
        provider.region = 'fra1'
        provider.size = 's-1vcpu-1gb'
        provider.privatenetworking = true
      end

      server.vm.hostname = "pythonkindergarten"

      server.vm.provision "shell", inline: <<-SHELL

        echo "Installing .NET 5.0"
        wget https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
        sudo dpkg -i packages-microsoft-prod.deb

        sudo apt-get update
        sudo apt-get install -y apt-transport-https
        sudo apt-get install -y dotnet-sdk-5.0
        sudo apt-get install -y aspnetcore-runtime-5.0
        dotnet dev-certs https
        
        cp -r /vagrant/* $HOME
        echo "ls"
        ls
        cd vagrant
        echo "ls"
        ls
        sudo nohup ./MiniTwitApi.Server > out.log &
        echo "================================================================="
        echo "=                            DONE                               ="
        echo "================================================================="
        echo "Navigate in your browser to:"
        THIS_IP=`hostname -I | cut -d" " -f1`
        echo "http://${THIS_IP}:5000"
      SHELL
    end
    
    config.vm.provision "shell", privileged: false, inline: <<-SHELL
      sudo apt-get update
    SHELL

  end

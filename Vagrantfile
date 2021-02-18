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
  
    config.vm.define "pythonkindergarten", primary: true do |server|
      server.vm.provider :digital_ocean do |provider|
        provider.ssh_key_name = ENV["SSH_KEY_NAME"]
        provider.token = ENV["DIGITAL_OCEAN_TOKEN"]
        provider.image = 'ubuntu-18-04-x64'
        provider.region = 'fra1'
        provider.size = 's-1vcpu-1gb'
        provider.privatenetworking = true
      end
  
      server.vm.hostname = "pythonkindergarten"

      server.trigger.after :up do |trigger|
        trigger.info =  "Writing pythonkindergarten's IP to file..."
        trigger.ruby do |env,machine|
          remote_ip = machine.instance_variable_get(:@communicator).instance_variable_get(:@connection_ssh_info)[:host]
          File.write($ip_file, remote_ip)
        end 
      end

      server.vm.provision "shell", inline: <<-SHELL
        echo "Installing MongoDB"
        wget -qO - https://www.mongodb.org/static/pgp/server-4.2.asc | sudo apt-key add -
        echo "deb [ arch=amd64 ] https://repo.mongodb.org/apt/ubuntu bionic/mongodb-org/4.2 multiverse" | sudo tee /etc/apt/sources.list.d/mongodb-org-4.2.list
        sudo apt-get update
        # sudo apt-get install -y mongodb-org-shell mongodb-org-server mongodb-org-mongos mongodb-org
        sudo apt-get install -y mongodb-org

        sudo mkdir -p /data/db
        sudo sed -i '/  bindIp:/ s/127.0.0.1/0.0.0.0/' /etc/mongod.conf

        sudo systemctl start mongod
        mongorestore --gzip /vagrant/dump
      SHELL
    end

    config.vm.define "webserver", primary: false do |server|
  
      server.vm.provider :digital_ocean do |provider|
        provider.ssh_key_name = ENV["SSH_KEY_NAME"]
        provider.token = ENV["DIGITAL_OCEAN_TOKEN"]
        provider.image = 'ubuntu-18-04-x64'
        provider.region = 'fra1'
        provider.size = 's-1vcpu-1gb'
        provider.privatenetworking = true
      end

      server.vm.hostname = "webserver"

      server.trigger.before :up do |trigger|
        trigger.info =  "Waiting to create server until dbserver's IP is available."
        trigger.ruby do |env,machine|
          ip_file = "db_ip.txt"
          while !File.file?($ip_file) do
            sleep(1)
          end
          db_ip = File.read($ip_file).strip()
          puts "Now, I have it..."
          puts db_ip
        end 
      end

      server.trigger.after :provision do |trigger|
        trigger.ruby do |env,machine|
          File.delete($ip_file) if File.exists? $ip_file
        end 
      end

      server.vm.provision "shell", inline: <<-SHELL
        export DB_IP=`cat /vagrant/db_ip.txt`
        echo $DB_IP

        echo "Installing Anaconda..."
        sudo wget https://repo.anaconda.com/archive/Anaconda3-2019.07-Linux-x86_64.sh -O $HOME/Anaconda3-2019.07-Linux-x86_64.sh
    
        bash ~/Anaconda3-2019.07-Linux-x86_64.sh -b
        
        echo ". $HOME/.bashrc" >> $HOME/.bash_profile
        echo "export PATH=$HOME/anaconda3/bin:$PATH" >> $HOME/.bash_profile
        export PATH="$HOME/anaconda3/bin:$PATH"
        rm Anaconda3-2019.07-Linux-x86_64.sh
        source $HOME/.bash_profile

        echo $DB_IP


        pip install Flask-PyMongo


        cp -r /vagrant/* $HOME
        nohup python minitwit.py > out.log &
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

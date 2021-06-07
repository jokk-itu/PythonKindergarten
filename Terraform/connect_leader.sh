terraform apply
ssh root@$(terraform output -raw minitwit-swarm-leader-ip-address) -i ssh_key/terraform
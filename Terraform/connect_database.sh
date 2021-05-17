terraform apply
ssh root@$(terraform output -raw minitwit-swarm-database-ip-address) -i ssh_key/terraform
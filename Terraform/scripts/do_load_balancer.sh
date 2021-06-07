#!/bin/bash
load_balancer_name="Pythonkindergarten-LB"

droplet_id_array=$(jq --argjson arr1 "$(terraform output -json minitwit-swarm-manager-id)" --argjson arr2 "$(terraform output -json minitwit-swarm-worker-id)" --argjson arr3 "[$(terraform output -json minitwit-swarm-leader-id)]" -n '$arr1 + $arr2 + $arr3' | tr -d '"')
echo $droplet_id_array

# Create new load balancer

curl -X POST -H "Content-Type: application/json" -H "Authorization: Bearer $TF_VAR_do_token" -d '{ "name": "'"$load_balancer_name"'", "region": "fra1", "forwarding_rules": [ { "entry_protocol": "http", "entry_port": 80, "target_protocol": "http", "target_port": 80 }, { "entry_protocol": "https", "entry_port": 443, "target_protocol": "https", "target_port": 443, "tls_passthrough": true } ], "health_check": { "protocol": "https", "port": 443, "path": "/", "check_interval_seconds": 10, "response_timeout_seconds": 5, "healthy_threshold": 5, "unhealthy_threshold": 3 }, "droplet_ids": '"$droplet_id_array"' }' https://api.digitalocean.com/v2/load_balancers

# Get the load balancer ip after it has been initialized


lb_list_response=$(curl -X GET -H "Content-Type: application/json" -H "Authorization: Bearer $TF_VAR_do_token" "https://api.digitalocean.com/v2/load_balancers")
floating_ip=$(echo $lb_list_response | jq -c ".load_balancers | to_entries[] | select(.value.name == \"$load_balancer_name\") | .value.ip")

while [[ "$floating_ip" == "\"\"" ]]
do
    sleep 5
    lb_list_response=$(curl -X GET -H "Content-Type: application/json" -H "Authorization: Bearer $TF_VAR_do_token" "https://api.digitalocean.com/v2/load_balancers")
    floating_ip=$(echo $lb_list_response | jq -c ".load_balancers | to_entries[] | select(.value.name == \"$load_balancer_name\") | .value.ip")
done

echo "Adding domain with A record to $floating_ip"
sleep 5

# Add pythonkindergarten.tech domain
curl -X POST -H "Content-Type: application/json" -H "Authorization: Bearer $TF_VAR_do_token" -d '{"name":"pythonkindergarten.tech","ip_address":'"$floating_ip"'}' "https://api.digitalocean.com/v2/domains"
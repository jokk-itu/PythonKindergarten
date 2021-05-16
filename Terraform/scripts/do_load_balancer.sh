#!/bin/bash
droplet_id_array=$(jq --argjson arr1 "$(terraform output -json minitwit-swarm-manager-id)" --argjson arr2 "$(terraform output -json minitwit-swarm-worker-id)" --argjson arr3 "[$(terraform output -json minitwit-swarm-leader-id)]" -n '$arr1 + $arr2 + $arr3' | tr -d '"')
echo $droplet_id_array

# Create new load balancer

curl -X POST -H "Content-Type: application/json" -H "Authorization: Bearer $TF_VAR_do_token" -d '{ "name": "Pythonkindergarten-LB", "region": "fra1", "forwarding_rules": [ { "entry_protocol": "http", "entry_port": 80, "target_protocol": "http", "target_port": 80 }, { "entry_protocol": "https", "entry_port": 443, "target_protocol": "https", "target_port": 443, "tls_passthrough": true } ], "health_check": { "protocol": "https", "port": 443, "path": "/", "check_interval_seconds": 10, "response_timeout_seconds": 5, "healthy_threshold": 5, "unhealthy_threshold": 3 }, "droplet_ids": '"$droplet_id_array"' }' https://api.digitalocean.com/v2/load_balancers

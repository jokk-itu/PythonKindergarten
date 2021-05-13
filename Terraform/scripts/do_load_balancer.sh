#!/bin/bash

droplets=$1;

echo $droplets;

# Create new load balancer
#curl -X POST \
#  -H "Content-Type: application/json" \
#  -H "Authorization: Bearer $TF_VAR_do_token" \
#  -d '{ "name": "Pythonkindergarten-LB", "region": "fra1", "forwarding_rules": [ { "entry_protocol": "http", "entry_port": 80, "target_protocol": "http", "target_port": 80 }, { "entry_protocol": "https", "entry_port": 443, "target_protocol": "https", "target_port": 443, "tls_passthrough": true } ], "health_check": { "protocol": "https", "port": 443 } "droplet_ids": [ 3164444, 3164445 ] }' \
#  "https://api.digitalocean.com/v2/load_balancers"


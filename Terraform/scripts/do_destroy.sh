#!/bin/bash
echo "Destroying load balancer....."
load_balancer_name="Pythonkindergarten-LB"
lb_list_response=$(curl -X GET -H "Content-Type: application/json" -H "Authorization: Bearer $TF_VAR_do_token" "https://api.digitalocean.com/v2/load_balancers")
lb_id=$(echo $lb_list_response | jq -c ".load_balancers | to_entries[] | select(.value.name == \"$load_balancer_name\") | .value.id" | tr -d '"')

curl -X DELETE -H "Content-Type: application/json" -H "Authorization: Bearer $TF_VAR_do_token" "https://api.digitalocean.com/v2/load_balancers/$lb_id"

echo "Load balancer destroyed"


echo "Removing domain pythonkindergarten.tech..."

curl -X DELETE -H "Content-Type: application/json" -H "Authorization: Bearer $TF_VAR_do_token" "https://api.digitalocean.com/v2/domains/pythonkindergarten.tech"

echo "Domain removed"
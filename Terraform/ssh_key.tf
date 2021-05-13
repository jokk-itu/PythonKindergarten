
# add the ssh key
resource "digitalocean_ssh_key" "pythonkindergarten" {
  name = "pythonkindergarten"
  public_key = file(var.pub_key)
}

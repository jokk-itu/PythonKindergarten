using Newtonsoft.Json;

namespace MiniTwitApi.Shared.Models
{
    // {'username': username, 'email': email, 'pwd': pwd}
    public class UserDTO
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("username")] public string Username { get; set; }
        [JsonProperty("email")] public string Email { get; set; }
        [JsonProperty("pwd")] public string Password { get; set; }
    }
}
namespace MiniTwitApi.Shared.Models
{
    // {'username': username, 'email': email, 'pwd': pwd}
    public class UserDTO
    {
        [JsonProperty("username")] string Username { get; set; }
        [JsonProperty("email")] string Email { get; set; }
        [JsonProperty("pwd")] string Password { get; set; }
    }
}
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MiniTwitApi.Shared.Models
{
    // {'username': username, 'email': email, 'pwd': pwd}
    public class UserDTO
    {
        [JsonProperty("id")] 
        public int Id { get; set; }
        
        [JsonProperty("username")] 
        [StringLength(30, ErrorMessage = "Identifier too long (30 character limit).")]
        public string Username { get; set; }

        [JsonProperty("email")] 
        public string Email { get; set; }

        [JsonProperty("pwd")] 
        [StringLength(30, ErrorMessage = "Identifier too long (30 character limit).")]
        public string Password { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MiniTwitApi.Shared.Models.UserModels
{
    public class LoginUserDTO
    {
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
using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MiniTwitApi.Shared.Models 
{
    public class CreateUserDTO 
    {
        public CreateUserDTO() {}

        [JsonProperty("username")]
        [StringLength(30, ErrorMessage = "Identifier too long (30 character limit).")]
        public string Username {get; set;}

        [JsonProperty("pwd")]
        [StringLength(30, ErrorMessage = "Identifier too long (30 character limit).")]
        public string Password {get; set;}

        [JsonProperty("email")]
        public string Email {get; set;}

    }
}
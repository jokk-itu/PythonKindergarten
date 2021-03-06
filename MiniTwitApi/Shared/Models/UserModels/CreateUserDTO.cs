using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MiniTwitApi.Shared.Models 
{
    public class CreateUserDTO 
    {
        public CreateUserDTO() {}

        [JsonPropertyName("username")]
        [StringLength(30, ErrorMessage = "Identifier too long (30 character limit).")]
        public string Username {get; set;}

        [JsonPropertyName("pwd")]
        [StringLength(30, ErrorMessage = "Identifier too long (30 character limit).")]
        public string Password {get; set;}

        [JsonPropertyName("email")]
        public string Email {get; set;}

    }
}
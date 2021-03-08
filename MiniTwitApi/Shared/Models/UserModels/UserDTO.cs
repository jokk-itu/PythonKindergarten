using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace MiniTwitApi.Shared.Models.UserModels
{
    // {'username': username, 'email': email, 'pwd': pwd}
    public class UserDTO
    {
        [JsonPropertyName("id")] 
        public int Id { get; set; }
        
        [JsonPropertyName("username")] 
        [StringLength(30, ErrorMessage = "Identifier too long (30 character limit).")]
        public string Username { get; set; }

        [JsonPropertyName("email")] 
        [EmailAddress]
        public string Email { get; set; }

        [JsonPropertyName("pwd")]
        [StringLength(30, ErrorMessage = "Identifier too long (30 character limit).")]
        public string Password { get; set; }
        
        public static string GenerateProfilePictureLink(string email)
        {
            var fixedEmail = email.Trim().ToLower();
            Console.WriteLine($"This is the email to be hashed: {fixedEmail}");
            var hash = MD5Hash(fixedEmail);
            var link = $"http://www.gravatar.com/avatar/{hash}?d=identicon&s=48";
            Console.WriteLine(link);
            return link;
        }
        
        public static string MD5Hash(string input)
        {
            var md5Hash = MD5.Create();
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();
            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
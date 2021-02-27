using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

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

        public string GenerateProfilePictureLink()
        {
            string hash = this.Email.Trim(); 
            hash = hash.ToLower();
            hash = MD5Hash(hash);
            string link = "https://www.gravatar.com/avatar/" + hash;
            return link;
        }

        //Retrieved from https://www.c-sharpcorner.com/article/hashing-passwords-in-net-core-with-tips/    
        public static string MD5Hash(string input)
        {
            using(var sha256 = SHA256.Create())  
            {  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));  
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();    
                return hash;  
            }  
        }
    }
}
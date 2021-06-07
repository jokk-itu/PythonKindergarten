using System;
using System.IO;

namespace MiniTwitApi.Shared
{ 
    public static class DockerSecretHelper{
        public static string GetSecretOrEnvVar(string key)
        {
            var secretPath = $"/run/secrets/{key}";

            if(File.Exists(secretPath)){
                var secret = File.ReadAllText(secretPath);
                Console.WriteLine($"Found secret({key}): {secret}");
                return secret;
            }
            
            Console.WriteLine($"Failed to find secret with key: {key}");
            return null;
        }
        
    }

}
using SecureWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.Helpers
{
    public class Validator
    {
        public static Dictionary<string, string> Register(UserVM user)
        {
            var response = Validator.Login(user);
            
            if (string.IsNullOrEmpty(user.Email))
            {
                response.Add("email", "Email cannot be null");
            }

            return response;
        }

        public static Dictionary<string, string> Login(UserVM user)
        {
            var response = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(user.UserName))
            {
                response.Add("username", "User name cannot be null");
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                response.Add("password", "Password cannot be null");
            }            

            return response;
        }
    }
}

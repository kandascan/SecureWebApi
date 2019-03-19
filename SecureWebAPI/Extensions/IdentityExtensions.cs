using System.Security.Claims;
using System.Security.Principal;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using SecureWebAPI.DataAccess.Entities;
using System.Runtime.Serialization;

namespace SecureWebAPI.Extensions
{
    [DataContract(Name = "MyResource", Namespace = "http://example.org/resources")]
    public sealed class MyResourceType
    {
        private string text;
        private int number;

        public MyResourceType(string text, int number)
        {
            this.text = text;
            this.number = number;
        }

        [DataMember]
        public string Text { get { return this.text; } set { this.text = value; } }
        [DataMember]
        public int Number { get { return this.number; } set { this.number = value; } }
    }
    public static class IdentityExtensions
    {
        public static async Task<string> GenerateJwtToken(this UserEntity user, IConfiguration configuration, int[] userTeams)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim("UserTeams", string.Join(",",userTeams))
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                configuration["JwtIssuer"],
                configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static string GetUserId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);

            return claim?.Value ?? string.Empty;

        }
        public static string GetName(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(ClaimTypes.Name);

            return claim?.Value ?? string.Empty;
        }
    }
}
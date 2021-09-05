using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace skills.AuthProvider
{
   public class Auth : IAuth
   {
      private readonly string _username = "Demo1";
      private readonly string _password = "Demo1";
      private readonly IConfiguration _configuration;
      public Auth(IConfiguration configuration)
      {
         _configuration = configuration;
      }
      public string Authentication(string username, string password)
      {
         if (!(_username.Equals(username) || _password.Equals(password)))
         {
            return null;
         }

         // 1. Create Security Token Handler
         var tokenHandler = new JwtSecurityTokenHandler();

         // 2. Create Private Key to Encrypted
         var tokenKey = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);

         //3. Create JETdescriptor
         var tokenDescriptor = new SecurityTokenDescriptor()
         {
            Subject = new ClaimsIdentity(new[] { new Claim("id", username) }),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                 new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
         };
         //4. Create Token
         var token = tokenHandler.CreateToken(tokenDescriptor);

         // 5. Return Token from method
         return tokenHandler.WriteToken(token);
      }
   }
}

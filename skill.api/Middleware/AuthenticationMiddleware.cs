using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using skill.repository.Interface;
using skill.manager;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.Middleware
{
   public class AuthenticationMiddleware
   {
      private readonly RequestDelegate _next;
      private readonly IConfiguration _configuration;

      public AuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
      {
         _next = next;
         _configuration = configuration;
      }

      public async Task Invoke(HttpContext context, IUserIdentityRepository userIdentityRepository)
      {
         var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

         if (token != null)
            AttachAccountToContext(context, token, userIdentityRepository);
         
         await _next(context);
      }


      private async void AttachAccountToContext(HttpContext context, string token, IUserIdentityRepository userIdentityRepository)
      {
         try
         {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(key),
               ValidateIssuer = true,
               ValidIssuer = _configuration["Jwt:Issuer"],
               ValidateAudience = true,
               ValidAudience = _configuration["Jwt:Audience"],
               // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
               ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);            

            var jwtToken = (JwtSecurityToken)validatedToken;
        
            var accountId = jwtToken.Claims.First(x => x.Type == "id").Value;
            var orgId = jwtToken.Claims.First(x => x.Type == "orgId").Value;
            var userType = jwtToken.Claims.First(x => x.Type == "userType").Value;
            var buId = jwtToken.Claims.Where(x => x.Type == "BUID").Select(x => x.Value).FirstOrDefault();
            if (context.Items["Org_Id"] == null)
            {
               // attach account to context on successful jwt validation
               context.Items["Org_Id"] = orgId;
               context.Items["UserId"] = accountId;
               context.Items["UserType"] = userType;
               if(buId !=null)
               context.Items["BUID"] = buId;
            }
            
         }
         catch (Exception ex)
         {
            throw;
         }
      }
   }
}

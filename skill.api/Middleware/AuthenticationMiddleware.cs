﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using skills.manager;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skills.Middleware
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

      public async Task Invoke(HttpContext context)
      {
         var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

         if (token != null)
            AttachAccountToContext(context, token);

         await _next(context);
      }


      private void AttachAccountToContext(HttpContext context, string token)
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

            // attach account to context on successful jwt validation
            context.Items["User"] = "test";
         }
         catch(Exception ex)
         {
            
         }
      }
   }
}
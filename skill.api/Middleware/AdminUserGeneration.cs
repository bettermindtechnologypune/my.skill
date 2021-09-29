using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using skill.manager.Interface;
using skill.manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace skill.Middleware
{
   public class AdminUserGeneration
   {
      private readonly RequestDelegate _next;
      private readonly IConfiguration _configuration;
      private readonly IUserManager _userManager;

      public AdminUserGeneration(RequestDelegate next, IConfiguration configuration,IUserManager userManager)
      {
         _next = next;
         _configuration = configuration;
         _userManager = userManager;
      }

      public async Task Invoke(HttpContext context)
      {
         var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

         if (token != null)
            //AttachAccountToContext(context, token);

         await _next(context);
      }
   }
}

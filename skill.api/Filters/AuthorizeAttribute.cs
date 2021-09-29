using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using skill.common.Enum;
using skill.repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skill.Filters
{

   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
   public class AuthorizeAttribute : Attribute, IAuthorizationFilter
   {
      //IUserIdentityRepository _userIdentityRepository;
      //public AuthorizeAttribute(IUserIdentityRepository userIdentityRepository)
      //{
      //   _userIdentityRepository = userIdentityRepository;
      //}
      private readonly IList<UserType> _roles;

      public AuthorizeAttribute(params UserType[] roles)
      {
         _roles = roles ?? new UserType[] { };
      }
      public void OnAuthorization(AuthorizationFilterContext context)
      {
         var account = context.HttpContext.Items["UserId"];
         if (account == null || (_roles.Any() && !_roles.Contains((UserType)context.HttpContext.Items["Usertype"])))
         {
            // not logged in
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
         }
      }

   }
}

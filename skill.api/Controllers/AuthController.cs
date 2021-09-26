using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using skills.AuthProvider;
using skills.common.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skills.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class AuthController : Controller
   {
      readonly IAuth _auth;
      public AuthController(IAuth auth)
      {
         _auth = auth;
      }
      [AllowAnonymous]
      [HttpPost(nameof(Authentication))]
      [Produces("application/json", "application/xml")]
      public async Task<IActionResult> Authentication([FromBody] Login login)
      {
         var token = await _auth.Authentication(login.UserName, login.Password);
         if (token == null)
            return Unauthorized();
         return Ok(token);
      }
   }
}

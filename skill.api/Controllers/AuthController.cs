using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using skill.AuthProvider;
using skill.common.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace skill.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class AuthController : Controller
   {
      readonly IAuth _auth;
      private readonly ILogger<AuthController> _logger;
      public AuthController(IAuth auth, ILogger<AuthController> logger)
      {
         _auth = auth;
         _logger = logger;
      }
      [AllowAnonymous]
      [HttpPost(nameof(Authentication))]
      [Produces("application/json", "application/xml")]
      public async Task<IActionResult> Authentication([FromBody] Login login)
      {
         try
         {
            _logger.LogInformation("AuthController::Authentication-Started");
            var authResponseModel = await _auth.Authentication(login.UserName, login.Password);
            if (authResponseModel == null)
               return Unauthorized();
            return StatusCode((int)HttpStatusCode.OK, authResponseModel);
         }
         catch(Exception ex)
         {
            _logger.LogError("AuthController::"+ex.Message, ex.InnerException);
            _logger.LogError($"AuthController::StackTrace", ex.InnerException);
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
         }
      }
   }
}

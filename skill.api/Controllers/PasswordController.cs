using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using skill.common.Model;
using skill.manager.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skills.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class PasswordController : ControllerBase
   {
      IPasswordManager _passwordResetRequestManager;
      private readonly ILogger<PasswordController> _logger;
      public PasswordController(IPasswordManager passwordResetRequestManager, ILogger<PasswordController> logger)
      {
         _passwordResetRequestManager = passwordResetRequestManager;
         _logger = logger;
      }

      [HttpGet]
      [Route("forget-password/{email}")]
      public async Task<IActionResult> ForgetPasswordRequest(string email)
      {
         try
         {
            var result =await _passwordResetRequestManager.RegisterPasswordResetRequest(email);
            if (result)
               return Ok(result);
            return NotFound();
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex.InnerException);
            return StatusCode(500, ex.Message);
         }

      }

      [HttpPost]
      [Route("reset-password")]
      public async Task<IActionResult> ResetPassword([FromBody] ChangePassword changePassword)
      {
         try
         {
            var result = await _passwordResetRequestManager.ResetPassword(changePassword);
            if (result)
               return Ok(result);
            return NotFound();
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex.InnerException);
            return StatusCode(500, ex.Message);
         }

      }
   }
}

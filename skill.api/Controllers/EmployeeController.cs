using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using skill.common.Enum;
using skill.common.Model;
using skill.Filters;
using skill.manager.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skills.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class EmployeeController : ControllerBase
   {
      private readonly ILogger<EmployeeController> _logger;
      IEmployeeManager _employeeManager;

      public EmployeeController(ILogger<EmployeeController> logger, IEmployeeManager employeeManager)
      {
         _logger = logger;
         _employeeManager = employeeManager;
      }

      [Authorize(UserType.Hr_Admin)]
      [HttpPost(nameof(Create))]
      public async Task<IActionResult> Create([FromBody] EmployeeResource resource)
      {
         try
         {
            if (resource != null)
            {
               var result = await _employeeManager.Create(resource);
               return Ok(result);
            }
            else
            {
               return StatusCode(400, "Employee is null");
            }
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex.InnerException);
            return StatusCode(500, ex.Message);
         }
      }
   }
}

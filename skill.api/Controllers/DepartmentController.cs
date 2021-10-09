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
   public class DepartmentController : ControllerBase
   {
      private readonly ILogger<DepartmentController> _logger;

      IDepartmentManager _departmentManager;

      public DepartmentController(ILogger<DepartmentController> logger, IDepartmentManager departmentManager)
      {
         _logger = logger;
         _departmentManager = departmentManager;
      }

      [Authorize(UserType.Hr_Admin)]
      [HttpPost(nameof(Create))]
      public async Task<IActionResult> Create([FromBody] DepartmentResource resource)
      {
         try
         {
            if (resource != null)
            {
               var result = await _departmentManager.Create(resource);
               return Ok(result);
            }
            else
            {
               return StatusCode(400, "BusinessUnit is null");
            }
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex.InnerException);
            return StatusCode(500, ex.Message);
         }
      }


      [Authorize(UserType.Hr_Admin)]
      [HttpGet(nameof(GetList))]
      public IActionResult GetList()
      {
         try
         {
            var result = _departmentManager.GetList();
            if (result != null)
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

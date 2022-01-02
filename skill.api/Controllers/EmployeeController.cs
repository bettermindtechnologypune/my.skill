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

      //[Authorize(UserType.Hr_Admin, UserType.Manager, UserType.Worker)]
      [HttpGet]
      [Route("{managerId}")]
      public IActionResult GetListByManagerId(Guid managerId,[FromQuery] EmployeePaginationModel employeePaginationModel)
      {
         try
         {
            var result = _employeeManager.GetListByManagerId(managerId, employeePaginationModel.PageNumber, employeePaginationModel.PageSize, employeePaginationModel.SearchText);
            if (result != null)
               return Ok(result);
            return null;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex.InnerException);
            return StatusCode(500, ex.Message);
         }

      }

      [HttpGet]
      [Route("manager-list/{deptId}")]
      public IActionResult GetManagerListByDeptId(Guid deptId)
      {
         try
         {
            var result = _employeeManager.GetListByDepartmentId(deptId);
            if (result != null)
               return Ok(result);
            return null;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex.InnerException);
            return StatusCode(500, ex.Message);
         }

      }

      [HttpGet]
      [Route("skill-measurement/{buid}")]
      public async Task<IActionResult> GetEmployeeSkillMeasurement(Guid buid)
      {
         try
         {
            var result = await _employeeManager.GetEmployeeSkillMeasurement(buid);
            if (result != null)
            {
               HttpContext.Response.Headers.Add("content-disposition", "attachment; filename=EmployeeSkillMeasurement" + DateTime.Now.Year.ToString() + ".xls");
               this.Response.ContentType = "application/vnd.ms-excel";
               byte[] temp = System.Text.Encoding.UTF8.GetBytes(result.ToString());
               return File(temp, "application/vnd.ms-excel");
            }
            return null;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex.InnerException);
            return StatusCode(500, ex.Message);
         }

      }
   }
}

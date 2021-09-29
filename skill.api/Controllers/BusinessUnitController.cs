using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using skill.common.Enum;
using skill.common.Model;
using skill.Controllers;
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
   public class BusinessUnitController : ControllerBase
   {
      private readonly ILogger<BusinessUnitController> _logger;
      IBusinessUnitManager _businessUnitManager;

      public BusinessUnitController(ILogger<BusinessUnitController> logger, IBusinessUnitManager businessUnitManager)
      {
         _logger = logger;
         _businessUnitManager = businessUnitManager;
      }

      [Authorize(UserType.Super_Admin, UserType.Org_Admin)]
      [HttpPost(nameof(Create))]     
      public async Task<IActionResult> Create([FromBody] BusinessUnitResource resource)
      {
         try
         {
            if (resource != null)
            {
               var result = await _businessUnitManager.Create(resource);
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
   }
}

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
   public class LeveTwoController : ControllerBase
   {
      private readonly ILogger<LeveTwoController> _logger;

      IlevelTwoManager _levelTwoManager;

      public LeveTwoController(ILogger<LeveTwoController> logger, IlevelTwoManager levelTwoManager)
      {
         _logger = logger;
         _levelTwoManager = levelTwoManager;
      }

      [Authorize(UserType.Hr_Admin)]
      [HttpPost(nameof(Create))]
      public async Task<IActionResult> Create([FromBody] List<LevelTwoResource> resources)
      {
         try
         {
            if (resources.Any())
            {
               var result = await _levelTwoManager.Create(resources);
               return Ok(result);
            }
            else
            {
               return StatusCode(400, "Level two is null");
            }
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex.InnerException);
            return StatusCode(500, ex.Message);
         }
      }


      [Authorize(UserType.Hr_Admin, UserType.Manager, UserType.Worker)]
      [HttpGet]
      [Route("{levelOneId}")]
      public IActionResult GetList(Guid levelOneId)
      {
         try
         {
            var result = _levelTwoManager.GetLevelTwoListByLevelOneId(levelOneId);
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

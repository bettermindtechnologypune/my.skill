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
   public class LevelOneController : ControllerBase
   {
      private readonly ILogger<LevelOneController> _logger;

      ILevelOneManager _levelOneManager;

      public LevelOneController(ILogger<LevelOneController> logger, ILevelOneManager levelOneManager)
      {
         _logger = logger;
         _levelOneManager = levelOneManager;
      }


      [Authorize(UserType.Hr_Admin)]
      [HttpPost(nameof(Create))]
      public async Task<IActionResult> Create([FromBody] List<LevelOneResource> resources)
      {
         try
         {
            if (resources.Any())
            {
               var result = await _levelOneManager.Create(resources);
               return Ok(result);
            }
            else
            {
               return StatusCode(400, "Level one is null");
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
      [Route("{buid}")]
      public IActionResult GetList(Guid buid)
      {
         try
         {
            var result = _levelOneManager.GetLevelOneListByBUID(buid);
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

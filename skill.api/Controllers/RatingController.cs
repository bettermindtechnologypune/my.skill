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
   public class RatingController : ControllerBase
   {
      private readonly ILogger<RatingController> _logger;

      IRatingManager _ratingManager;

      public RatingController(ILogger<RatingController> logger, IRatingManager ratingManager)
      {
         _logger = logger;
         _ratingManager = ratingManager;
      }

      //[Authorize(UserType.Hr_Admin, UserType.Manager, UserType.Worker)]
      [HttpPost(nameof(CreateList))]
      public async Task<IActionResult> CreateList([FromBody] List<RatingResource> resources)
      {
         try
         {
            if (resources.Any())
            {
               var result = await _ratingManager.CreateBulk(resources);
               return Ok(result);
            }
            else
            {
               return StatusCode(400, "Ratng is null");
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
      [Route("list-by-empId-ratingName/{empId}/{ratingName}")]
      public IActionResult GetListByEmpIdAndRatingName(Guid empId, string ratingName)
      {
         try
         {
            var result = _ratingManager.GetEmployeeRatingModel(empId, ratingName);
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

      [Authorize(UserType.Hr_Admin, UserType.Manager, UserType.Worker)]
      [HttpGet]
      [Route("{taskId}")]
      public IActionResult GetListByTadkId(Guid taskId)
      {
         try
         {
            var result = _ratingManager.GetListByTaskId(taskId);
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

      //[Authorize(UserType.Hr_Admin, UserType.Manager, UserType.Worker)]
      [HttpGet]
      [Route("rating-names/{empId}")]
      public IActionResult GetRatingNamesByEmpId(Guid empId)
      {
         try
         {
            var result = _ratingManager.GetRatingNameByEmpId(empId);
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

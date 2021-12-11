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
   public class TaskController : ControllerBase
   {

      private readonly ILogger<TaskController> _logger;

      ITaskManager _taskManager;

      public TaskController(ILogger<TaskController> logger, ITaskManager taskManager)
      {
         _logger = logger;
         _taskManager = taskManager;
      }

      [Authorize(UserType.Hr_Admin)]
      [HttpPost(nameof(CreateList))]
      public async Task<IActionResult> CreateList([FromBody] List<TaskResource> resources)
      {
         try
         {
            if (resources.Any())
            {
               var result = await _taskManager.CreateBulk(resources);
               return Ok(result);
            }
            else
            {
               return StatusCode(400, "Task is null");
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
      [Route("{levelId}")]
      public IActionResult GetList(Guid levelId)
      {
         try
         {
            var result = _taskManager.GetTaskListByLevelId(levelId);
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
      [HttpPatch]      
      [Route("{taskId}")]
      public async Task<IActionResult> Update(Guid taskId, [FromBody] TaskResource taskResource)
      {
         try
         {
            var result =await _taskManager.Update(taskId,taskResource);
            if (result == true)
               return Ok(result);
            return NotFound();
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex.InnerException);
            return StatusCode(500, ex.Message);
         }

      }

      [HttpPatch(nameof(Update))]      
      public async Task<IActionResult> Update([FromBody] List<TaskResource> taskResources)
      {
         try
         {
            var result = await _taskManager.UpdateListAsync(taskResources);
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

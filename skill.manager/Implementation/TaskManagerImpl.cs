using skill.common.CustomException;
using skill.common.Model;
using skill.common.TenantContext;
using skill.manager.Interface;
using skill.manager.Mapper;
using skill.manager.Validator.Interface;
using skill.repository.Entity;
using skill.repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Implementation
{
   public class TaskManagerImpl : ITaskManager
   {
      ITenantContext _tenantContext;
      ITaskRepository _taskRepository;
      ITaskValidator _taskValidator;

      public TaskManagerImpl(ITaskRepository taskRepository, ITenantContext tenantContext, ITaskValidator taskValidator)
      {
         _taskRepository = taskRepository;
         _tenantContext = tenantContext;
         _taskValidator = taskValidator;
      }

      Guid UserId
      {
         get
         {
            return _tenantContext == null ? Guid.Empty : _tenantContext.UserId;
         }
      }

      public async Task<List<TaskResource>> CreateBulk(List<TaskResource> resources)
      {
         try
         {
            List<TaskEntity> entities = new List<TaskEntity>();
            foreach (var resource in resources)
            {
               resource.Id = Guid.NewGuid();
               var entity = TaskMapper.ToEntity(resource);
               entity.CreatedBy = UserId;
               entity.CreatedDate = DateTime.UtcNow;
               entities.Add(entity);
            }

            var result = await _taskRepository.BulkInsertAsync(entities);
            if (result != null && result.Any())
               return resources;
         }
         catch (Exception)
         {
            throw;
         }

         return null;
      }

      public List<TaskResource> GetTaskListByLevelId(Guid levelId)
      {
               try
               {
                  var entities = _taskRepository.GetTaslListByLevelId(levelId);
                  List<TaskResource> taskResource = null;
                  if (entities.Any())
                  {
                     taskResource = new List<TaskResource>();
                     foreach (var entity in entities)
                     {
                        taskResource.Add(TaskMapper.ToResource(entity));
                     }
                  }

                  return taskResource;
               }
               catch (Exception)
               {
                  throw;
               }
      }

      public async Task<bool> Update(Guid taskId, TaskResource taskResource)
      {
         var existingEnity =await _taskRepository.GetAsync(taskId);

         if(existingEnity !=null && existingEnity.Id == taskId)
         {
            if(taskResource.Name !=null)
            {
               existingEnity.Name = taskResource.Name;
            }
            if (taskResource.Wattage != 0)
            {
               existingEnity.Wattage = taskResource.Wattage;
            }

            existingEnity.ModifiedBy = UserId;
            existingEnity.ModifiedDate = DateTime.UtcNow;
            
            _taskRepository.UpdateAsync(existingEnity);

            return true;
         }

        else
         {
            throw new Exception($"Task with name: {taskResource.Name} does not exists");
         }
      }


      public async Task<List<TaskResource>> UpdateListAsync(List<TaskResource> taskResources)
      {
         List<TaskEntity> taskEntities = new List<TaskEntity>();
         var errors = _taskValidator.ValidateTaskList(taskResources);
         if(errors.Any())
         {
            throw new ValidationException(string.Join(",", errors));
         }
         foreach (var task in taskResources)
         {
            var taskEnitiy = TaskMapper.ToEntity(task);
            if (task.Id != Guid.Empty)
            {
               
               taskEnitiy.ModifiedBy = UserId;
               taskEnitiy.ModifiedDate = DateTime.UtcNow;
               taskEntities.Add(taskEnitiy);
            }
            else
            {              
               taskEnitiy.CreatedBy = UserId;
               taskEnitiy.CreatedDate = DateTime.UtcNow;
               await _taskRepository.InsertAsync(taskEnitiy);
            }
         }

         var result =await _taskRepository.UpdateListAsync(taskEntities);
         if(result.Any())
         {
            return taskResources;
         }
         return null;
      }
   }
}

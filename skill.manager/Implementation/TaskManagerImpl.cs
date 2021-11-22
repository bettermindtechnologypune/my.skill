using skill.common.Model;
using skill.common.TenantContext;
using skill.manager.Interface;
using skill.manager.Mapper;
using skill.repository.Entity;
using skill.repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Implementation
{
   public class TaskManagerImpl : ITaskManager
   {
      ITenantContext _tenantContext;
      ITaskRepository _taskRepository;

      public TaskManagerImpl(ITaskRepository taskRepository, ITenantContext tenantContext)
      {
         _taskRepository = taskRepository;
         _tenantContext = tenantContext;
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
   }
}

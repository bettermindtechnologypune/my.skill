using skill.common.Model;
using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Mapper
{
   public static class TaskMapper
   {
      public static TaskEntity ToEntity(TaskResource resource)
      {
         if (resource == null)
            return null;

         TaskEntity entity = new TaskEntity
         {
            Id = resource.Id,
            LevelId = resource.LevelId,
            Name = resource.Name,
            Wattage = resource.Wattage
         };

         return entity;
      }

      public static TaskResource ToResource(TaskEntity entity)
      {
         if (entity == null)
            return null;

         TaskResource resource = new TaskResource
         {
            Id = entity.Id,
            LevelId = entity.LevelId,
            Name = entity.Name,
            Wattage = entity.Wattage
         };

         return resource;
      }
   }
}

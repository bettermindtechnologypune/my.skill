using skill.common.Model;
using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Mapper
{
   public static class RatingMapper
   {
      public static RatingEntity ToEntity(RatingResource resource)
      {
         if (resource == null)
            return null;

         RatingEntity entity = new RatingEntity
         {
            Id = resource.Id,
            EmpId = resource.EmpId,
            IsManagerRating = resource.IsManagerRating,
            Rating = resource.Rating,
            TaskId = resource.TaskId,
            Name = resource.Name
         };

         return entity;
      }

      public static RatingResource ToResource(RatingEntity entity)
      {
         if (entity == null)
            return null;

         RatingResource resource = new RatingResource
         {
            Id = entity.Id,
            EmpId = entity.EmpId,
            IsManagerRating = entity.IsManagerRating,
            Rating = entity.Rating,
            TaskId = entity.TaskId,
            Name = entity.Name
         };

         return resource;
      }
   }
}

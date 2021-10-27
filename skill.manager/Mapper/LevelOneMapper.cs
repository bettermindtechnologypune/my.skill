using skill.common.Model;
using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Mapper
{
   public static class LevelOneMapper
   {
      public static LevelOneEntity ToEntity(LevelOneResource resource)
      {
         if (resource == null)
            return null;

         LevelOneEntity entity = new LevelOneEntity
         {
            BUID = resource.BUID,
            Id = resource.Id,
            IsLastLevel = resource.IsLastLevel,
            Name = resource.Name
         };

         return entity;
      }

      public static LevelOneResource ToResource(LevelOneEntity entity)
      {
         if (entity == null)
            return null;

         LevelOneResource resource = new LevelOneResource
         {
            BUID = entity.BUID,
            Id = entity.Id,
            IsLastLevel = entity.IsLastLevel,
            Name = entity.Name
         };

         return resource;
      }
   }
}

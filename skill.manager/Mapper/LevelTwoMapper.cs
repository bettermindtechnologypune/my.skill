using skill.common.Model;
using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Mapper
{
  public static class LevelTwoMapper
   {
      public static LevelTwoEntity ToEntity(LevelTwoResource resource)
      {
         if (resource == null)
            return null;

         LevelTwoEntity entity = new LevelTwoEntity
         {
            LevelOneId = resource.LevelOneId,
            Id = resource.Id,
            IsLastLevel = resource.IsLastLevel,
            Name = resource.Name
         };

         return entity;
      }

      public static LevelTwoResource ToResource(LevelTwoEntity entity)
      {
         if (entity == null)
            return null;

         LevelTwoResource resource = new LevelTwoResource
         {
            LevelOneId = entity.LevelOneId,
            Id = entity.Id,
            IsLastLevel = entity.IsLastLevel,
            Name = entity.Name,
            BUID = entity.BUID
         };

         return resource;
      }
   }
}

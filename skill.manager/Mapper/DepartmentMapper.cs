using skill.common.Model;
using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Mapper
{
   public class DepartmentMapper
   {
      public static DepartmentEntity ToEntity(DepartmentResource resource)
      {
         if (resource == null)
            return null;

         DepartmentEntity entity = new DepartmentEntity()
         {
            BusinessUnitId = resource.BusinessUnitId,
            Id = resource.Id,           
            Name = resource.Name
         };

         return entity;
      }

      public static DepartmentResource ToResource(DepartmentEntity entity)
      {
         if (entity == null)
            return null;

         DepartmentResource resource = new DepartmentResource()
         {
            BusinessUnitId = entity.BusinessUnitId,
            Id = entity.Id,           
            Name = entity.Name
         };

         return resource;
      }
   }
}

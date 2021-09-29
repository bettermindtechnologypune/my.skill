using skill.common.Model;
using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Mapper
{
   public class BusinessUnitMapper
   {
      public static BusinessUnitEntity ToEntity(BusinessUnitResource resource)
      {
         if (resource == null)
         {
            return null;
         }
         BusinessUnitEntity entity = new BusinessUnitEntity()
         {            
            City = resource.City,
            CompanyAddress = resource.CompanyAddress,
            ContactNumber = resource.ContactNumber,
            Email = resource.Email,           
            Id = resource.Id,
            Name = resource.Name,
            PostalCode = resource.PostalCode,
            State = resource.State,
            WebSite = resource.WebSite
         };

         return entity;
      }

      public static BusinessUnitResource ToResource(BusinessUnitEntity entity)
      {
         if (entity == null)
         {
            return null;
         }
         BusinessUnitResource resource = new BusinessUnitResource()
         {
            City = entity.City,
            CompanyAddress = entity.CompanyAddress,
            ContactNumber = entity.ContactNumber,
            Email = entity.Email,
            Id = entity.Id,
            Name = entity.Name,
            PostalCode = entity.PostalCode,
            State = entity.State,
            WebSite = entity.WebSite
         };

         return resource;
      }
   }
}

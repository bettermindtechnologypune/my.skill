
using skill.repository.Entity;
using skill.common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace skill.manager.Mapper
{
   public class OrganizationMapper
   {
      public static OrganizationEntity ToEntity(OrganizationResource resource)
      {
         if(resource == null)
         {
            return null;
         }
         OrganizationEntity entity = new OrganizationEntity()
         {
            BillingEmail = resource.BillingEmail,
            City = resource.City,
            CompanyAddress = resource.CompanyAddress,
            ContactNumber = resource.ContactNumber,
            Email = resource.Email,
            HasMultipleBU = resource.HasMultipleBU,
            Id = resource.Id,
            Name = resource.Name,
            PostalCode = resource.PostalCode,
            State = resource.State,
            WebSite = resource.WebSite
         };

         return entity;
      }

      public static OrganizationResource ToResource(OrganizationEntity entity)
      {
         if (entity == null)
         {
            return null;
         }
         OrganizationResource resource = new OrganizationResource()
         {
            BillingEmail = entity.BillingEmail,
            City = entity.City,
            CompanyAddress = entity.CompanyAddress,
            ContactNumber = entity.ContactNumber,
            Email = entity.Email,
            HasMultipleBU = entity.HasMultipleBU,
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

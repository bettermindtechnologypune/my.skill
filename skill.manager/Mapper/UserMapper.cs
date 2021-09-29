using skill.repository.Entity;
using skill.common.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Mapper
{
   public class UserMapper
   {
      public static UserEntity ToEntity(UserResource resource)
      {
         if (resource == null)
            return null;

         UserEntity entity = new UserEntity
         {
            Contact = resource.Contact,
            Createdby = resource.Createdby,
            CreatedTime = resource.CreatedTime,
            Email = resource.Email,
            Id = resource.Id,
            IdentityUserId = resource.IdentityUserId,
            IsDeleted = resource.IsDeleted,
            IsOrgAdmin = resource.IsOrgAdmin,
            Modifiedby = resource.Modifiedby,
            ModifiedTime = resource.ModifiedTime,
            Name = resource.Name,
            OrgId = resource.OrgId,
            UserCode = resource.UserCode,
            UserName = resource.UserName,
            UserType = resource.UserType             
         };

         return entity;
      }

      public static UserResource ToResource(UserEntity entity)
      {
         if (entity == null)
            return null;

         UserResource resource = new UserResource
         {
            Contact = entity.Contact,
            Createdby = entity.Createdby,
            CreatedTime = entity.CreatedTime,
            Email = entity.Email,
            Id = entity.Id,
            IdentityUserId = entity.IdentityUserId,
            IsDeleted = entity.IsDeleted,
            IsOrgAdmin = entity.IsOrgAdmin,
            Modifiedby = entity.Modifiedby,
            ModifiedTime = entity.ModifiedTime,
            Name = entity.Name,
            OrgId = entity.OrgId,
            UserCode = entity.UserCode,
            UserName = entity.UserName,
            UserType = entity.UserType
         };

         return resource;
      }
   }
}

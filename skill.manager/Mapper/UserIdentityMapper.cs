using skill.repository.Entity;
using skill.common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Mapper
{
   public class UserIdentityMapper  
   {
      public static UserIdentityEntity ToEntity(UserIdentityResource resource)
      {
         if(resource == null)
         {
            return null;
         }

         UserIdentityEntity entity = new UserIdentityEntity
         {
            Email = resource.Email,
            FailedLoginCount= resource.FailedLoginCount,
            FullName = resource.FullName,
            Id = resource.Id,
            CreatedAt = resource.CreatedAt,
            IsDeleted = resource.IsDeleted,
            IsLoginLocked = resource.IsLoginLocked,
            LastLoginAttemptAt = resource.LastLoginAttemptAt,
            LastSuccessfulLoginAt = resource.LastSuccessfulLoginAt,
            ModifiedAt = resource.ModifiedAt,
            Name = resource.Name,
            Password = resource.Password,
            PasswordChangedAt = resource.PasswordChangedAt,
            PhoneNumber = resource.PhoneNumber,
            UserCode = resource.UserCode
         };

         return entity;
      }

      public static UserIdentityResource ToResource(UserIdentityEntity entity)
      {
         if (entity == null)
            return null;

         UserIdentityResource resource = new UserIdentityResource
         {
            Email = entity.Email,
            FailedLoginCount = entity.FailedLoginCount,
            FullName = entity.FullName,
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            IsDeleted = entity.IsDeleted,
            IsLoginLocked = entity.IsLoginLocked,
            LastLoginAttemptAt = entity.LastLoginAttemptAt,
            LastSuccessfulLoginAt = entity.LastSuccessfulLoginAt,
            ModifiedAt = entity.ModifiedAt,
            Name = entity.Name,
            Password = entity.Password,
            PasswordChangedAt = entity.PasswordChangedAt,
            PhoneNumber = entity.PhoneNumber,
            UserCode = entity.UserCode
         };

         return resource;
      }
   }
}

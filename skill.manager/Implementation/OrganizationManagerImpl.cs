using skill.common.Enum;
using skill.common.Model;
using skill.manager.Interface;
using skill.repository.Entity;
using skill.repository.Interface;
using skills.common.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Implementation
{
   public class OrganizationManagerImpl : IOrganizationManager
   {
      IOrganizationRepository _organizationRepository;
      IUserIdentityRepository _userIdentityRepository;
      private readonly IEmailSettingsRepository _emailSettingsRepository;
      private readonly IEmailManager _emailManager;
      public OrganizationManagerImpl(IOrganizationRepository organizationRepository, IUserIdentityRepository userIdentityRepository,
         IEmailSettingsRepository emailSettingsRepository, IEmailManager emailManager)
      {
         _organizationRepository = organizationRepository;
         _userIdentityRepository = userIdentityRepository;
         _emailSettingsRepository = emailSettingsRepository;
         _emailManager = emailManager;
      }
      public async  Task<bool> CreateOrganization(OrganizationEntity entity)
      {
         entity.CreateDate = DateTime.UtcNow;
         entity.Id = Guid.NewGuid();
         entity.CreatedBy = Guid.NewGuid().ToString();

         var isOrgSuccess = await _organizationRepository.InsertAsync(entity);

         if(isOrgSuccess == true)
         {
            var key = await _emailSettingsRepository.GetSymmetricKey();
            var plainText = AesOperation.RandomPassword();
            var encPassword = AesOperation.EncryptString(key, plainText);
            var userEntity = new UserIdentityEntity
            {
               PhoneNumber = entity.ContactNumber,
               CreatedBy= Guid.NewGuid(),
               CreatedAt = DateTime.UtcNow,               
               Email = entity.Email,
               Id = Guid.NewGuid(),
               FullName =entity.Name,
               OrgId = Guid.NewGuid(),
               Name = entity.Name,
               IsOrgAdmin = true,
               UserType = UserType.Org_Admin,
               Password = encPassword,
               IsDeleted = false,
               IsLoginLocked = false,               
            };

            await _userIdentityRepository.CreateUserIdentity(userEntity);

            var emailRequest = new EmailRequest();

            emailRequest.ToEmail = entity.Email;
            emailRequest.Subject = "Default Password for System Admin";
            emailRequest.Body = $"Hi {entity.Email},/n Your username is {entity.Email} and Password for skill application login is {plainText}. Please do not share the password with any one, Thank you!!";

            await _emailManager.SendEmailAsync(emailRequest);
         }
         return true;
      }
   }
}

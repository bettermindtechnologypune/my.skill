using skill.common.Enum;
using skill.common.Model;
using skill.common.TenantContext;
using skill.manager.Interface;
using skill.manager.Mapper;
using skill.repository.Entity;
using skill.repository.Interface;
using skill.common.Model;
using skill.common.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using skill.manager.Validator.Interface;
using System.ComponentModel.DataAnnotations;

namespace skill.manager.Implementation
{
   public class OrganizationManagerImpl : IOrganizationManager
   {
      IOrganizationRepository _organizationRepository;
      IUserIdentityRepository _userIdentityRepository;
      private readonly IEmailSettingsRepository _emailSettingsRepository;
      private readonly IEmailManager _emailManager;
      ITenantContext _tenantContext;
      IValidator<OrganizationResource> _validator;

      public OrganizationManagerImpl(IOrganizationRepository organizationRepository, IUserIdentityRepository userIdentityRepository,
         IEmailSettingsRepository emailSettingsRepository, IEmailManager emailManager, ITenantContext tenantContext, IValidator<OrganizationResource> validator)
      {
         _organizationRepository = organizationRepository;
         _userIdentityRepository = userIdentityRepository;
         _emailSettingsRepository = emailSettingsRepository;
         _emailManager = emailManager;
         _tenantContext = tenantContext;
         _validator = validator;
      }

      Guid OrgId
      {
         get
         {
            return _tenantContext == null ? Guid.Empty : _tenantContext.OrgId;
         }
      }

      Guid UserId         
      {
         get
         {
            return _tenantContext == null ? Guid.Empty : _tenantContext.UserId;
         }
      }
      public async  Task<bool> CreateOrganization(OrganizationResource resource)
      {
         var errors = _validator.Validate(resource);
         if(errors.Any())
         {
            throw new ValidationException(string.Join(",", errors));
         }
         resource.Id = Guid.NewGuid();     

         var entity = OrganizationMapper.ToEntity(resource);
         entity.CreateDate = DateTime.UtcNow;
         entity.CreatedBy = UserId.ToString();

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

            return true;
         }
         return false;
      }
   }
}

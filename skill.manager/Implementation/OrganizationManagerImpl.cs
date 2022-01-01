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
      IBusinessUnitRepository _businessUnitRepository;
      ITenantContext _tenantContext;
      IOrganizationValidator _validator;

      public OrganizationManagerImpl(IOrganizationRepository organizationRepository, IUserIdentityRepository userIdentityRepository,
         IEmailSettingsRepository emailSettingsRepository, IEmailManager emailManager, ITenantContext tenantContext, IOrganizationValidator validator,
         IBusinessUnitRepository businessUnitRepository)
      {
         _organizationRepository = organizationRepository;
         _userIdentityRepository = userIdentityRepository;
         _emailSettingsRepository = emailSettingsRepository;
         _emailManager = emailManager;
         _tenantContext = tenantContext;
         _validator = validator;
         _businessUnitRepository = businessUnitRepository;
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
      public async Task<OrganizationResource> CreateOrganization(OrganizationResource resource)
      {
         var errors = _validator.Validate(resource);
         if (errors.Any())
         {
            throw new ValidationException(string.Join(",", errors));
         }
         resource.Id = Guid.NewGuid();

         var entity = OrganizationMapper.ToEntity(resource);
         entity.CreatedDate = DateTime.UtcNow;
         entity.CreatedBy = UserId.ToString();

         var response = await _organizationRepository.InsertAsync(entity);       

         if (response != null && response.Id != Guid.Empty)
         {
            var userEntity = await CreateUser(resource);
            if(userEntity?.Id != Guid.Empty && resource.HasMultipleBU == false)
            {
               var bu = new BusinessUnitEntity
               {
                  ContactNumber = resource.ContactNumber,
                  CompanyAddress = resource.CompanyAddress,               
                  City = resource.City,
                  CreatedBy = UserId.ToString(),
                  CreatedDate = DateTime.UtcNow,
                  Email = resource.Email,
                  Id = Guid.NewGuid(),
                  Name = resource.Name,
                  OrgId = resource.Id,
                  PostalCode = resource.PostalCode,
                  State = resource.State,
                  WebSite = resource.WebSite                  
               };

               await _businessUnitRepository.InsertAsync(bu);
            }
            return resource;
         }
         return null;
      }

      private async Task<UserIdentityEntity> CreateUser(OrganizationResource organizationResource)
      {
         try
         {
            var key = await _emailSettingsRepository.GetSymmetricKey();
            var plainText = AesOperation.RandomPassword();
            var encPassword = AesOperation.EncryptString(key, plainText);
            var userEntity = new UserIdentityEntity
            {
               PhoneNumber = organizationResource.ContactNumber,
               CreatedBy = UserId,
               CreatedDate = DateTime.UtcNow,
               Email = organizationResource.Email,
               Id = Guid.NewGuid(),
               FullName = organizationResource.Name,
               OrgId = organizationResource.Id,
               Name = organizationResource.Name,
               IsOrgAdmin = true,
               UserType = organizationResource.HasMultipleBU == true? UserType.Org_Admin:UserType.Hr_Admin,
               Password = encPassword,
               IsDeleted = false,
               IsLoginLocked = false,
            };

            await _userIdentityRepository.CreateUserIdentity(userEntity);

            var emailRequest = new EmailRequest();

            emailRequest.ToEmail = organizationResource.Email;
            emailRequest.Subject = $"Default Password for {organizationResource.Name}'s Admin";
            emailRequest.Body = $"Hi {organizationResource.Name},/n Your username is {organizationResource.Email} and Password for skill application login is {plainText}. Please do not share the password with any one, Thank you!!";

            await _emailManager.SendEmailAsync(emailRequest);

            return userEntity;
         }
         catch
         {
            throw;
         }
      }

      public async Task<List<BusinessUnitSkillModel>> GetBUSkillLevel()
      {
         return await _organizationRepository.GetBUSkillLevel(OrgId);
      }
   }
}

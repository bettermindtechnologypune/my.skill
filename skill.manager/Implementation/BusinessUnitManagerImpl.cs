using skill.common.Enum;
using skill.common.Model;
using skill.common.Operation;
using skill.common.TenantContext;
using skill.manager.Interface;
using skill.manager.Mapper;
using skill.manager.Validator.Interface;
using skill.repository.Entity;
using skill.repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Implementation
{
   public class BusinessUnitManagerImpl : IBusinessUnitManager
   {
      IBusinessUnitRepository _businessUnitRepository;
      ITenantContext _tenantContext;
      IBusinessUnitValidator _validator;
      IUserIdentityRepository _userIdentityRepository;
      private readonly IEmailSettingsRepository _emailSettingsRepository;
      private readonly IEmailManager _emailManager;
      public BusinessUnitManagerImpl(IBusinessUnitRepository businessUnitRepository, ITenantContext tenantContext, IBusinessUnitValidator validator,
         IUserIdentityRepository userIdentityRepository,
         IEmailSettingsRepository emailSettingsRepository, IEmailManager emailManager)
      {
         _businessUnitRepository = businessUnitRepository;
         _tenantContext = tenantContext;
         _validator = validator;
         _userIdentityRepository = userIdentityRepository;
         _emailSettingsRepository = emailSettingsRepository;
         _emailManager = emailManager;
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

      public async Task<BusinessUnitResource> Create(BusinessUnitResource resource)
      {
         var errors = _validator.Validate(resource);       
         if (errors.Any())
         {
            throw new ValidationException(string.Join(",", errors));
         }
         resource.Id = Guid.NewGuid();
         var entity = BusinessUnitMapper.ToEntity(resource);
         entity.CreatedBy = UserId.ToString();
         entity.OrgId = OrgId;
         entity.CreatedDate = DateTime.UtcNow;
        
         var result = await _businessUnitRepository.InsertAsync(entity);
         var BUResource = BusinessUnitMapper.ToResource(result);
         if (BUResource != null && BUResource.Id != Guid.Empty)
         {
            var key = await _emailSettingsRepository.GetSymmetricKey();
            var plainText = AesOperation.RandomPassword();
            var encPassword = AesOperation.EncryptString(key, plainText);
            var userEntity = new UserIdentityEntity
            {
               PhoneNumber = entity.ContactNumber,
               CreatedBy = UserId,
               CreatedDate = DateTime.UtcNow,
               Email = entity.Email,
               Id = Guid.NewGuid(),
               FullName = entity.Name,
               OrgId = OrgId,
               Name = entity.Name,
               IsOrgAdmin = false,
               UserType = UserType.Hr_Admin,
               Password = encPassword,
               IsDeleted = false,
               IsLoginLocked = false,
               BUID = entity.Id
            };

            await _userIdentityRepository.CreateUserIdentity(userEntity);

            var emailRequest = new EmailRequest();

            emailRequest.ToEmail = entity.Email;
            emailRequest.Subject = "Default Password for Businesss unit Admin";
            emailRequest.Body = $"Hi {entity.Name},/n Your username is {entity.Email} and Password for skill application login is {plainText} . Please do not share the password with any one, Thank you!!";

            await _emailManager.SendEmailAsync(emailRequest);

            return BUResource;
         }

         return null;

      }

      public async Task<BusinessUnitResource> GetByAdminId(Guid adminId)
      {
         BusinessUnitResource resource = null;
         var entity = await _businessUnitRepository.GetByAdminId(adminId);
         if (entity != null && entity.Id != Guid.Empty)
         {
            resource = BusinessUnitMapper.ToResource(entity);
         }

         return resource;
      }

      public async Task<BusinessUnitResource> GetById(Guid id)
      {
         BusinessUnitResource resource = null;
         var entity = await _businessUnitRepository.GetAsync(id);
         if (entity != null && entity.Id != Guid.Empty)
         {
            resource = BusinessUnitMapper.ToResource(entity);
         }

         return resource;
      }
   }
}

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
   public class EmployeeManagerImpl : IEmployeeManager
   {
      ITenantContext _tenantContext;
      IEmployeeRepository _employeeRepository;
      IUserIdentityRepository _userIdentityRepository;
      private readonly IEmailSettingsRepository _emailSettingsRepository;
      private readonly IEmailManager _emailManager;
      IEmployeeValidator _validator;
      public EmployeeManagerImpl(IEmployeeRepository employeeRepository, ITenantContext tenantContext,
         IUserIdentityRepository userIdentityRepository, IEmployeeValidator validator,
         IEmailSettingsRepository emailSettingsRepository, IEmailManager emailManager)
      {
         _employeeRepository = employeeRepository;
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

      Guid BUID
      {
         get
         {
            return _tenantContext == null ? Guid.Empty : _tenantContext.BUId;
         }
      }

      public async Task<EmployeeResource> Create(EmployeeResource resource)
      {
         var errors = _validator.Validate(resource);
         if (errors.Any())
         {
            throw new ValidationException(string.Join(",", errors));
         }

         resource.Id = Guid.NewGuid();
         resource.BUID = BUID;
         var entity = EmployeeMapper.ToEntity(resource);
         entity.CreatedBy = UserId.ToString();
         entity.CreatedDate = DateTime.UtcNow;
         var result = await _employeeRepository.InsertAsync(entity);
         if (result !=null && result.Id !=Guid.Empty)
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
               FullName = $"{entity.FirstName} {entity.LastName}",
               OrgId = OrgId,
               Name = entity.FirstName,
               IsOrgAdmin = false,
               UserType = UserType.Worker,
               Password = encPassword,
               IsDeleted = false,
               IsLoginLocked = false,
               BUID = BUID
            };

            await _userIdentityRepository.CreateUserIdentity(userEntity);

            var emailRequest = new EmailRequest();

            emailRequest.ToEmail = entity.Email;
            emailRequest.Subject = "Default Password for Skill Application";
            emailRequest.Body = $"Hi {entity.FirstName},/n Your username is {entity.Email} and Password for skill application login is {plainText} . Please do not share the password with any one, Thank you!!";

            await _emailManager.SendEmailAsync(emailRequest);

            return resource;
         }

         return null;
      }
   }
}

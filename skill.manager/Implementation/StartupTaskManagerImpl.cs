using Microsoft.Extensions.Configuration;
using skill.common.Enum;
using skill.common.Model;
using skill.manager.Interface;
using skill.repository.Entity;
using skill.repository.Implementation;
using skill.repository.Interface;
using skill.common.Model;
using skill.common.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Implementation
{
   public class StartupTaskManagerImpl : IStartupTaskManager
   {
      private readonly IEmailManager _emailManager;
      private readonly IConfiguration _configuration;
      private readonly IEmailSettingsRepository _emailSettingsRepository;
      private readonly IUserIdentityRepository _userIdentityRepository;
    
     
      public StartupTaskManagerImpl(IEmailManager emailManager, IConfiguration configuration,
         IEmailSettingsRepository emailSettingsRepository, IUserIdentityRepository userIdentityRepository)
      {
         _emailManager = emailManager;
         _configuration = configuration;
         _emailSettingsRepository = emailSettingsRepository;
         _userIdentityRepository = userIdentityRepository;       
      }

      public async Task<bool> Execute()
      {
         try
         {
            var userIdentity = await _userIdentityRepository.GetUserIdentityByEmail(_configuration["auc:email"]);

            if (userIdentity == null)
            {
               userIdentity = new UserIdentityEntity
               {
                  Email = _configuration["auc:email"],
                  CreatedDate = DateTime.UtcNow,
                  FailedLoginCount = 0,
                  FullName = _configuration["auc:name"],
                  Id = Guid.NewGuid(),
                  IsDeleted = false,
                  IsLoginLocked = false,
                  Name = _configuration["auc:name"],
                  Password = _configuration["auc:password"],
                  IsOrgAdmin = true,
                  OrgId = Guid.NewGuid(),
                  UserType = UserType.Super_Admin
               };

               await _userIdentityRepository.CreateUserIdentity(userIdentity);
               
               var emailRequest = new EmailRequest();
               var name = _configuration["auc:name"];
               emailRequest.ToEmail = _configuration["auc:email"];
               emailRequest.Subject = "Default Password for System Admin";
               var passwordDecrypted = _configuration["auc:password"];
               var key = await _emailSettingsRepository.GetSymmetricKey();
               var password = AesOperation.DecryptString(key, passwordDecrypted);
               emailRequest.Body = $"Hi {name},/n Your username is {emailRequest.ToEmail} and Password for skill application login is {password}. Please do not share the password with any one, Thank you!!";

               await _emailManager.SendEmailAsync(emailRequest);

            }

            return true;
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message);
         }
      }
   }
}

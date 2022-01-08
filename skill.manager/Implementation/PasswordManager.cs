using skill.common.Model;
using skill.common.Operation;
using skill.manager.Interface;
using skill.manager.Validator.Implementation;
using skill.repository.Entity;
using skill.repository.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace skill.manager.Implementation
{
   public class PasswordManager : IPasswordManager
   {
      IPasswordResetRequestRepository _passwordResetRequestRepository;
      IEmailManager _emailManager;
      IEmailSettingsRepository _emailSettingsRepository;
      IUserIdentityRepository _userIdentityRepository;
      IPasswordValidator _validator;

      public PasswordManager(IPasswordResetRequestRepository passwordResetRequestRepository, IEmailManager emailManager,
         IEmailSettingsRepository emailSettingsRepository, IUserIdentityRepository userIdentityRepository, IPasswordValidator validator)
      {
         _passwordResetRequestRepository = passwordResetRequestRepository;
         _emailManager = emailManager;
         _emailSettingsRepository = emailSettingsRepository;
         _userIdentityRepository = userIdentityRepository;
         _validator = validator;
      }

      public async Task<bool> RegisterPasswordResetRequest(string email)
      {
         try
         {
            var userIdentity = await _userIdentityRepository.GetUserIdentityByEmail(email);
            if (userIdentity == null && string.IsNullOrEmpty(userIdentity.Email))
            {
               throw new Exception("Email is invalid");
            }

            _passwordResetRequestRepository.DeleteByLogin(email);

            Random generator = new Random();
            string uniqueResetCode = generator.Next(0, 1000000).ToString("D6");

            PasswordResetRequestEntity passwordResetRequestEntity = new PasswordResetRequestEntity
            {
               CreatedDate = DateTime.UtcNow,
               Id = Guid.NewGuid(),
               Login = email,
               UserId = userIdentity.Id,
               ResetCode = uniqueResetCode
            };

            await _passwordResetRequestRepository.InsertAsync(passwordResetRequestEntity);


            var emailRequest = new EmailRequest();

            emailRequest.ToEmail = email;
            emailRequest.Subject = "Password Reset Reqeust for Skills Application";
            var template = @$"<div>Hi {userIdentity.FullName},<br> 
                              There was a request to reset your password! <br> 
                              If you did not make this request then please ignore this email. <br> 
                              Otherwise, please enter the code <b>{uniqueResetCode}</b> in reset password page
                              <b>Note: request code is valid for 10 minutes</b></div>";

            emailRequest.Body = template;


            await _emailManager.SendEmailAsync(emailRequest);

            return true;

         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message, ex.InnerException);
         }
      }


      public async Task<bool> ResetPassword(ChangePassword changePassword)
      {
         const int expireMinutes = 10;
         if (string.IsNullOrEmpty(changePassword.ResetCode))
         {
            throw new Exception("Password reset code is not valid.");
         }
         var result = _passwordResetRequestRepository.GetPasswordResetRequestByResetCode(changePassword.ResetCode, changePassword.Email);

         if (result == null && string.IsNullOrEmpty(result.Login))
         {
            throw new Exception("Invalid password reset request");
         }

         int span = DateTime.UtcNow.Subtract(result.CreatedDate).Minutes;
         if (span > expireMinutes)
         {
            throw new Exception("Password reset request has expired.");
         }

         var userIdentiy = await _userIdentityRepository.GetUserIdentityByEmail(changePassword.Email);

         if (userIdentiy == null)
         {
            throw new Exception("User not found");
         }

         var errors = _validator.Validate(changePassword);
         if (errors.Any())
         {
            throw new ValidationException(string.Join(",", errors));
         }
         var key = await _emailSettingsRepository.GetSymmetricKey();

         var encPassword = AesOperation.EncryptString(key, changePassword.NewPassword);

         await _userIdentityRepository.UpdatePassword(encPassword);

         _passwordResetRequestRepository.DeleteByLogin(changePassword.Email);

         return true;
      }

      public async Task<bool> ChangePassword(ChangePassword changePassword)
      {
         var userIdentiy = await _userIdentityRepository.GetUserIdentityById(changePassword.UserId);

         if(userIdentiy == null)
         {
            throw new Exception("User not found");
         }

         var errors = _validator.Validate(changePassword);
         if (errors.Any())
         {
            throw new ValidationException(string.Join(",", errors));
         }
         var key = await _emailSettingsRepository.GetSymmetricKey();

         var encPassword = AesOperation.EncryptString(key, changePassword.NewPassword);

         await _userIdentityRepository.UpdatePassword(encPassword);

         return true;
      }

   }
}

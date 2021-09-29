using skill.common.ErrorModel;
using skill.manager.Validator.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace skill.manager.Validator.Implementation
{
   public class BaseValidator<R> : IValidator<R> where R : class
   {

      public const string INVALID_EMAIL = "Invalid email address";
      public const string EMPTY_FIELD_ERROR = "{0} should not be empty.";
      public const string INVALID_PHONE = "Invalid contact number";

      public IList<Error> Validate(R resource)
      {
         throw new NotImplementedException();
      }

      public virtual Error ValidateEmail(string email)
      {
         Error error = null;
         if (IsValidLength(email, 255) && !string.IsNullOrEmpty(email) &&
            Regex.IsMatch(email, @"^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+[.])+[a-z]{2,5}$", RegexOptions.IgnoreCase))
         {
            return null;
         }
         else
         {
            
            error.Message = INVALID_EMAIL;
            return error;
         }

      }

      public virtual Error ValidatePhoneNumber(string phoneNumber)
      {
         
         const string CONTACT_NO = "Contact No.";
         Error error = null;
         if (string.IsNullOrEmpty(phoneNumber))
         {
            string msg = String.Format(EMPTY_FIELD_ERROR, CONTACT_NO);
            error.Message = msg;
         }
         if (!IsValidLength(phoneNumber, 30))
         {
            error.Message = INVALID_PHONE;
         }

         return error;
      }

      private bool IsValidLength(string input, int length)
      {
         return (input.Length <= length);
      }
   }
}

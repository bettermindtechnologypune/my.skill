using skill.common.ErrorModel;
using skill.common.Model;
using skill.manager.Validator.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace skill.manager.Validator.Interface
{
   public class PasswordValidator : BaseValidator<ChangePassword>, IPasswordValidator
   {
      List<Error> _Errors;
      const string error = @"Password must contain At least one lower case letter, At least one upper case letter,
                             At least special character,At least one number, At least 8 characters length";
      public override IList<Error> Validate(ChangePassword resource)
      {
         _Errors = new List<Error>();

         _Errors.Add(ValidatePassword(resource));
         _Errors.RemoveAll(item => item == null);
         return _Errors;         
      }

      private Error ValidatePassword(ChangePassword resource)
      {         
         if(Regex.IsMatch(resource.NewPassword, @"^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+[.])+[a-z]{2,5}$", RegexOptions.IgnoreCase))
         {
            return new Error { Message = error };
         }

         return null;
      }

   }
}

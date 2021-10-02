using skill.common.ErrorModel;
using skill.common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Validator.Implementation
{
   public class BusinessUnitValidationImpl : BaseValidator<BusinessUnitResource>
   {
      List<Error> _Errors;
      public new IList<Error> Validate(BusinessUnitResource resource)
      {
         _Errors = new List<Error>();

         _Errors.Add(ValidateEmail(resource.Email));      
         _Errors.Add(ValidatePhoneNumber(resource.ContactNumber));
         _Errors.RemoveAll(item => item == null);
         return _Errors;
      }


      public override Error ValidateEmail(string email)
      {
         return base.ValidateEmail(email);
      }

      public override Error ValidatePhoneNumber(string phoneNumber)
      {
         return base.ValidatePhoneNumber(phoneNumber);
      }
   }
}

using skill.common.ErrorModel;
using skill.common.Model;
using skill.manager.Validator.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Validator.Implementation
{
   public class OrganizationValidationImpl : BaseValidator<OrganizationResource>, IOrganizationValidator
   {
      List<Error> _Errors;
      public override IList<Error> Validate(OrganizationResource resource)
      {
         _Errors = new List<Error>();

         _Errors.Add(ValidateEmail(resource.Email));
         _Errors.Add(ValidateEmail(resource.BillingEmail));
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

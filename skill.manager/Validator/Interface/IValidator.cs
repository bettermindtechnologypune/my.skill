using skill.common.ErrorModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Validator.Interface
{
   public interface IValidator<R> where R : class
   {
      IList<Error> Validate(R resource);

      Error ValidateEmail(string email);

      Error ValidatePhoneNumber(string email);
   }
}

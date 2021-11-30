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
   public class TaskValidator : BaseValidator<EmployeeResource>, ITaskValidator
   {
      List<Error> _Errors;
      public IList<Error> Validate(TaskResource resource)
      {
         throw new NotImplementedException();
      }

      public IList<Error> ValidateTaskList(List<TaskResource> resourceList)
      {
         _Errors = new List<Error>();

         _Errors.Add(ValidateWatage(resourceList));

         _Errors.RemoveAll(item => item == null);
         return _Errors;
      }

      private Error ValidateWatage(List<TaskResource> resourceList)
      {
         int value = 0;
         Error error = null;
         foreach(var resource in resourceList)
         {
            if (resource.Wattage != 0)
            {
               value += resource.Wattage;
            }
         }

         if(value !=0 && value != 100)
         {
            error = new Error();
            error.Message = $"The total task watage must be 100, currently it is {value}";
         }

         return error;
      }
   }
}

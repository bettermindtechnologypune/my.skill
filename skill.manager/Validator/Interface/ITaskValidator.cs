using skill.common.ErrorModel;
using skill.common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Validator.Interface
{
   public interface ITaskValidator : IValidator<TaskResource>
   {
      public IList<Error> ValidateTaskList(List<TaskResource> resourceList);
      
   }
}

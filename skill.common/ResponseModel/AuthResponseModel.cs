using skill.common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.common.ResponseModel
{
   public class AuthResponseModel
   {
      public string Token { get; set; }

      public UserType UserType { get; set; }
   }
}

using skill.common.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skills.AuthProvider
{
   public interface IAuth
   {
      Task<AuthResponseModel> Authentication(string username, string password);
   }
}

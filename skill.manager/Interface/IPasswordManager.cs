using skill.common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Interface
{
   public interface IPasswordManager
   {
      Task<bool> RegisterPasswordResetRequest(string email);

      Task<bool> ResetPassword(ChangePassword changePassword);

      Task<bool> ChangePassword(ChangePassword changePassword);
   }
}

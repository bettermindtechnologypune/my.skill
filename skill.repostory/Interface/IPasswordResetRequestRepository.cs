using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Interface
{
   public interface IPasswordResetRequestRepository : IBaseRepositoy<PasswordResetRequestEntity>
   {
      void DeleteByLogin(string email);

      PasswordResetRequestEntity GetPasswordResetRequestByResetCode(string resetCode, string email);
   }
}

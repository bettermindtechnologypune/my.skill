using skill.common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Interface
{
   public  interface IUserManager
   {
      void CreateUser(UserResource userResource);

      Task<UserResource> GetByEmail(string email);
   }
}

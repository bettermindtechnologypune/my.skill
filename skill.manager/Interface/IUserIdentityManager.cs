using skill.common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skill.manager.Interface
{
   public interface IUserIdentityManager
   {

      Task CreateUserIdentity(UserIdentityResource userIdentityResource);

      Task<UserIdentityResource> GetUserIdentityByEmail(string email);
   }
}

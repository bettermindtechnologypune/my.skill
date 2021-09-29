using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Interface
{
   public interface IUserIdentityRepository
   {
      Task CreateUserIdentity(UserIdentityEntity userIdentityEntity);

      Task<UserIdentityEntity> GetUserIdentityByEmail(string email, string password = null);

      Task<UserIdentityEntity> GetUserIdentityById(string Id);
   }
}

using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Interface
{
   public interface IUserRepository
   {
      Task<UserEntity> GetByEmail(string email);

      void InsertAsync(UserEntity userEntity);
   }
}

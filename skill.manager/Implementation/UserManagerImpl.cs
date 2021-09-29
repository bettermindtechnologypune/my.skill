using skill.manager.Interface;
using skill.manager.Mapper;
using skill.repository.Interface;
using skill.common.Model;
using System;
using System.Threading.Tasks;

namespace skill.manager
{
   public class UserManagerImpl : IUserManager
   {
      IUserRepository _userRepository;

      public UserManagerImpl(IUserRepository userRepository)
      {
         _userRepository = userRepository;
      }

      public void CreateUser(UserResource userResource)
      {
         try
         {
            var userEntity = UserMapper.ToEntity(userResource);

             //_userRepository.InsertAsync(userEntity);
         }
         catch
         {
            throw;
         }
      }

      public async Task<UserResource> GetByEmail(string email)
      {
         var entity = await _userRepository.GetByEmail(email);
         var resource = UserMapper.ToResource(entity);

         return resource;
      }
   }
}

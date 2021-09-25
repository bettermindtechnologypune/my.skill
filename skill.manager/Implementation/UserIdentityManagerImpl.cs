using Microsoft.Extensions.Configuration;
using skill.manager.Mapper;
using skill.repository.Interface;
using skills.common.Model;
using System;
using System.Threading.Tasks;
using skill.manager.Interface;

namespace skill.manager.Implementation 
{
   public class UserIdentityManagerImpl : IUserIdentityManager
   {
      IUserIdentityRepository _userIdentityRepository;
      public UserIdentityManagerImpl(IUserIdentityRepository userIdentityRepository)
      {
         _userIdentityRepository = userIdentityRepository;
      }

      public async Task CreateUserIdentity(UserIdentityResource userIdentityResource)
      {
         try
         {
            var userIdentityEntity = UserIdentityMapper.ToEntity(userIdentityResource);

            if(userIdentityEntity !=null)
            {
               await _userIdentityRepository.CreateUserIdentity(userIdentityEntity);
            }

         }
         catch(Exception)
         {
            throw;
         }
      }

      public async Task<UserIdentityResource> GetUserIdentityByEmail(string email)
      {
         try
         {
            UserIdentityResource userIdentityResource = null;

            var entity = await _userIdentityRepository.GetUserIdentityByEmail(email);
            if(entity != null)
            {
               userIdentityResource = UserIdentityMapper.ToResource(entity);
            }

            return userIdentityResource;
         }
         catch
         {
            throw;
         }
      }
   }
}

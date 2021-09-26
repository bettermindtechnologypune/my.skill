using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using skill.repository.Entity;
using skill.repository.Implementation;
using skill.repository.Interface;
using System.Threading.Tasks;

namespace skill.repository.Implementation
{
   public class UserRepositoryImpl : BaseRepository<UserEntity>, IUserRepository
   {

      public UserRepositoryImpl(IConfiguration configuration,ApplicationDBContext applicationDBContext
          ) : base(applicationDBContext,configuration)
      {
         
      }

      //public override void InsertAsync(UserEntity userEntity)
      //{
      //   base.InsertAsync(userEntity);
      //}

      public async Task<UserEntity> GetByEmail(string email)
      {
        var entity = await entities.FirstOrDefaultAsync(x => x.Email == email);

         return entity;
      }
   }
}

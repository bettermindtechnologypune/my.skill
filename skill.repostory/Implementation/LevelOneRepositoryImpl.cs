using Microsoft.Extensions.Configuration;
using skill.repository.Entity;
using skill.repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Implementation
{
   public class LevelOneRepositoryImpl : BaseRepository<LevelOneEntity>, ILevelOneRepository
   {
      public LevelOneRepositoryImpl(IConfiguration configuration, ApplicationDBContext applicationDBContext) : base(applicationDBContext, configuration)
      {

      }

      public List<LevelOneEntity> GetLevelOneListByBUID(Guid buid)
      {
         try
         {
            return _entities.Where(x => x.BUID == buid).ToList();
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message, ex.InnerException);
         }

      }

      public override async Task<LevelOneEntity> InsertAsync(LevelOneEntity entity)
      {
         try
         {
            return await base.InsertAsync(entity);
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message, ex.InnerException);
         }
      }
   }
}

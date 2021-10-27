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
  public class LevelTwoRepositoryImpl : BaseRepository<LevelTwoEntity>, ILevelTwoRepository
   {
      public LevelTwoRepositoryImpl(IConfiguration configuration, ApplicationDBContext applicationDBContext) : base(applicationDBContext, configuration)
      {

      }

      public List<LevelTwoEntity> GetLevelOneListByLevelOneId(Guid levelOneId)
      {
         try
         {
            return _entities.Where(x => x.LevelOneId == levelOneId).ToList();
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message, ex.InnerException);
         }

      }
   }
}

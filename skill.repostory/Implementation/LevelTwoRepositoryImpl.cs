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

      public List<LevelTwoEntity> GetLevelOneListByBUID(Guid BUID)
      {
         try
         {
            var levelTwoList = (from l2 in _context.LevelTwo
                                join l1 in _context.LevelOne on l2.LevelOneId equals l1.Id

                                where l1.BUID == BUID
                                select new LevelTwoEntity
                                {
                                   Id = l2.Id,
                                   LevelOneId = l2.LevelOneId,
                                   Name = l2.Name,
                                   CreatedBy = l2.CreatedBy,
                                   CreatedDate = l2.CreatedDate,
                                   IsLastLevel = l2.IsLastLevel,
                                   ModifiedBy = l2.ModifiedBy,
                                   ModifiedDate = l2.ModifiedDate
                                }).ToList();

            return levelTwoList;
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message, ex.InnerException);
         }

      }
   }
}

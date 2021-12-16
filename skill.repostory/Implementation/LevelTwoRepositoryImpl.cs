using Microsoft.Extensions.Configuration;
using MySqlConnector;
using skill.common.Model;
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

      public async Task<List<LevelTwoSkillModel>> GetSkillIndexForLevelTwoByLevelOneId(Guid levelOneId)
      {
         List<LevelTwoSkillModel> levelTwoSkillModels = null;
         try
         {
            string query = @"select temp2.DId as LevelTwoId, temp2.Name as LevelTwoName, count(temp2.L1) as L1 , count(temp2.L2) as L2 , count(temp2.L3) as L3 , count(temp2.L4) as L4 
                           , count(temp2.L5) as L5 , count(temp2.L0) as L0 from
                           ( select * , case when temp1.cal >=0.51 and temp1.cal  <= 1.50 then 1 END as L1,
                            case when temp1.cal >=1.51 and temp1.cal  <= 2.50 then 2 END as L2,
                            case when temp1.cal >=2.51 and temp1.cal  <= 3.50 then 3 END as L3,
                            case when temp1.cal >=3.51 and temp1.cal  <= 4.50 then 4 END as L4,
                            case when temp1.cal >=4.51 and temp1.cal  <= 5 then 5 END as L5,
                            case when temp1.cal >=0 and temp1.cal  <= 0.50 then 0 END as L0
                           from
                           (select l2.Id DId, l2.Name, e.Id, sum(r.Rating * t.wattage)/100 cal from skill_db.levelone l1
                           right outer join skill_db.leveltwo l2 on l2.Id = l1.Id
                           left join skill_db.task t on t.levelId = l2.Id
                           left join skill_db.rating r on r.TaskId = t.Id
                           left join skill_db.employee e on e.Id = r.EmpId where l2.LevelOneId = @levelOneId
                           group by  e.Id, l2.Id) as temp1) as temp2 group by temp2.DId";

            using (var command = new MySqlCommand(query, Connection))
            {
               command.Parameters.AddWithValue("@levelOneId", levelOneId);

               await Connection.OpenAsync();
               using (var reader = await command.ExecuteReaderAsync())
               {
                  levelTwoSkillModels = new List<LevelTwoSkillModel>();
                  while (reader.Read())
                  {                     
                     var levelTwoSkillModel = new LevelTwoSkillModel
                     {
                        LevelTwoId = (Guid)reader["LevelTwoId"],
                        LevelTwoName = (string)reader["LevelTwoName"],
                        SkillLevelZeroCount = Convert.ToInt64(reader["L0"]),
                        SkillLevelOneCount = Convert.ToInt64(reader["L1"]),
                        SkillLevelTwoCount = Convert.ToInt64(reader["L2"]),
                        SkillLevelThreeCount = Convert.ToInt64(reader["L3"]),
                        SkillLevelFourCount = Convert.ToInt64(reader["L4"]),
                        SkillLevelFiveCount = Convert.ToInt64(reader["L5"]),
                     };

                     levelTwoSkillModels.Add(levelTwoSkillModel);
                  }
               }
            }

            return levelTwoSkillModels;
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message, ex.InnerException);
         }
      }
   }
}

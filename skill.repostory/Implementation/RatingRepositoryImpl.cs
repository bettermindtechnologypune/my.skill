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
   public class RatingRepositoryImpl : BaseRepository<RatingEntity>, IRatingRepository
   {
      public RatingRepositoryImpl(IConfiguration configuration, ApplicationDBContext applicationDBContext) : base(applicationDBContext, configuration)
      {

      }

      public List<RatingEntity> GetListByEmpId(Guid empId)
      {
         try
         {
            return _entities.Where(x => x.EmpId == empId).ToList();
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message, ex.InnerException);
         }
      }

      public List<RatingEntity> GetListByTaskId(Guid taskId)
      {
         try
         {
            return _entities.Where(x => x.TaskId == taskId).ToList();
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message, ex.InnerException);
         }
      }

      public async Task<RatingResponseModel> GetEmployeeRatingModel(Guid empId, string ratingName, Guid BUID)
      {
         try
         {
            List<RatingResponseModel> ratingResponseModels = null;
            RatingResponseModel ratingResponseModel = null;
            string query = @"select distinct Id as BUID, Name as BUName,secondLastLevel.RatingId  ,secondLastLevel.rating, secondLastLevel.ManagerRating , 
                               secondLastLevel.SecondLastLevelId, secondLastLevel.SecondLastLevelName, secondLastLevel.LastLevelId, secondLastLevel.LastLevelName
                              ,secondLastLevel.TaskName, secondLastLevel.TaskId from skill_db.business_unit bu inner join
                              (select distinct lastLevel.TaskName, lastLevel.TaskId ,lastLevel.RatingId,lastLevel.rating, lastLevel.ManagerRating, l2.Id as
                              SecondLastLevelId ,l2.BUID,  l2.Name SecondLastLevelName, LastLevel.LastLevelId, lastLevel.LastLevelName  from skill_db.levelOne as l2 
                              right outer join
                              (select distinct ra.Id as RatingId, ra.rating, ra.ManagerRating , ta.Name as TaskName, ta.Id as TaskId, coalesce(lev1.Id, lev2.ID) as LastLevelId, coalesce(lev1.BUID, lev2.LevelOneId) as PrevLevelId, coalesce(lev1.Name, lev2.Name) as LastLevelName from skill_db.Rating ra
                              inner join skill_db.task ta on ra.TaskId = ta.Id and ra.Name = @ratingName
                               and ra.EmpId = @empId 
                              left join skill_db.leveltwo lev2 on lev2.Id = ta.LevelID
                              left join skill_db.levelone lev1 on lev1.Id = ta.LevelID) as lastLevel
                               on lastLevel.PrevLevelId = l2.Id || lastLevel.PrevLevelId = l2.BUID ) secondLastLevel
                               on secondLastLevel.BUID = bu.Id and bu.Id  = @BUID ;";

            using (var command = new MySqlCommand(query, Connection))
            {
               command.Parameters.AddWithValue("@empId", empId);
               command.Parameters.AddWithValue("@BUID", BUID);
               command.Parameters.AddWithValue("@ratingName", ratingName);
               await Connection.OpenAsync();

               using (var reader = await command.ExecuteReaderAsync())
               {
                  ratingResponseModels = new List<RatingResponseModel>();
                  while (reader.Read())
                  {
                     if (ratingResponseModel == null)
                     {
                        ratingResponseModel = new RatingResponseModel
                        {
                           BUID = (Guid)reader["BUID"],
                           BUName = (string)reader["BUName"],
                           LevelOneId = (Guid)reader["SecondLastLevelId"],
                           LevelOneName = (string)reader["SecondLastLevelName"],
                           LevelTwoId = new Guid((string)reader["LastLevelId"]),
                           LevelTwoName = (string)reader["LastLevelName"],
                           RatingReponseList = new List<RatingReponse>
                           {
                                    new RatingReponse
                              {
                                 RatingId = (Guid)reader["RatingId"],
                                 TaskId = (Guid)reader["TaskId"],
                                 TaskName = (string)reader["TaskName"],
                                 EmpRating = Convert.ToInt32(reader["rating"]),
                                 MangerRating = Convert.ToInt32(reader["ManagerRating"])
                              }
                           }

                        };
                     }
                     else
                     {
                        RatingReponse ratingReponse = new RatingReponse
                        {
                            RatingId = (Guid)reader["RatingId"],
                            TaskId = (Guid)reader["TaskId"],
                           TaskName = (string)reader["TaskName"],
                           EmpRating = Convert.ToInt32(reader["rating"]),
                           MangerRating = reader["ManagerRating"] == DBNull.Value ? null : Convert.ToInt32(reader["ManagerRating"])
                        };

                        ratingResponseModel.RatingReponseList.Add(ratingReponse);
                     }

                     ratingResponseModels.Add(ratingResponseModel);
                  }
               }
            }


            return ratingResponseModel;
         }
         catch (Exception ex)
         {
            throw ex;
         }
         finally
         {
            if (Connection.State == ConnectionState.Open)
            {
               Connection.Close();
            }
         }
      }
   }
}

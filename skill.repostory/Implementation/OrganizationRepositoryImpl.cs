using Microsoft.Extensions.Configuration;
using skill.common.Model;
using skill.repository.Entity;
using skill.repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace skill.repository.Implementation
{
   public class OrganizationRepositoryImpl : BaseRepository<OrganizationEntity>, IOrganizationRepository
   {

      public OrganizationRepositoryImpl(IConfiguration configuration, ApplicationDBContext applicationDBContext) : base(applicationDBContext, configuration)
      {

      }

      public override async Task<OrganizationEntity> InsertAsync(OrganizationEntity entity)
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
      public void Get()
      {
         //Connection.OpenAsync();
      }

      public async Task<List<BusinessUnitSkillModel>> GetBUSkillLevel(Guid orgId)
      {
         List<BusinessUnitSkillModel> businessUnitSkillModels = null;
         try
         {
            string query = @"select bu.Id, bu.Name, (sum(temp5.MainValue)/count(bu.Id )) as BULevelSkill from business_unit bu
                              inner join 
                              (select lev1.Id ,lev1.Buid ,(sum(temp4.MainValue)/count(lev1.Id )) as MainValue from levelone lev1
                              inner join leveltwo lev2 on lev2.levelOneId = lev1.Id
                              inner join 
                              (select LevelTwoId, LevelTwoName, ( ((1*L1) + (2*L2) + (3*L3) + (4*L4) + (5*L5)) / (L0+L1+L2+L3+L4+L5)) as MainValue, (L0+L1+L2+L3+L4+L5) value  from
                              (select temp2.DId as LevelTwoId, temp2.Name as LevelTwoName, count(temp2.L1) as L1 , count(temp2.L2) as L2 , count(temp2.L3) as L3 , count(temp2.L4) as L4 
                           , count(temp2.L5) as L5 , count(temp2.L0) as L0 from
                           (select * , case when temp1.cal >=0.51 and temp1.cal  <= 1.50 then 1 END as L1,
                            case when temp1.cal >=1.51 and temp1.cal  <= 2.50 then 2 END as L2,
                            case when temp1.cal >=2.51 and temp1.cal  <= 3.50 then 3 END as L3,
                            case when temp1.cal >=3.51 and temp1.cal  <= 4.50 then 4 END as L4,
                            case when temp1.cal >=4.51 and temp1.cal  <= 5 then 5 END as L5,
                            case when temp1.cal >=0 and temp1.cal  <= 0.50 then 0 END as L0
                           from
                           (select l2.Id DId, l2.Name, e.Id, sum(r.Rating * t.wattage)/100 cal from business_unit bu 
                           inner join skill_db.levelone l1 on l1.BUID = bu.Id and bu.OrgId = @orgId
                           inner  join skill_db.leveltwo l2 on l2.LevelOneId = l1.Id
                           inner join skill_db.task t on t.levelId = l2.Id
                           inner join skill_db.rating r on r.TaskId = t.Id
                           inner join skill_db.employee e on e.Id = r.EmpId 
                           group by l1.Id, e.Id, l2.Id) as temp1) as temp2 group by temp2.DId) as temp3 )
                           temp4 on temp4.LeveltwoId= lev2.Id group by lev1.Id ) temp5
                           on temp5.buid = bu.Id  ";

            using (var command = new MySqlCommand(query, Connection))
            {
               command.Parameters.AddWithValue("@orgId", orgId);

               await Connection.OpenAsync();
               using (var reader = await command.ExecuteReaderAsync())
               {
                  businessUnitSkillModels = new List<BusinessUnitSkillModel>();
                  while (reader.Read())
                  {
                     var buSkillModel = new BusinessUnitSkillModel
                     {
                        BUID = reader["Id"] == DBNull.Value ? Guid.Empty :  (Guid)reader["Id"],
                        BusinessUnitName = reader["Name"] == DBNull.Value ? null : (string)reader["Name"],
                        BUSkillLevel = reader["BULevelSkill"] == DBNull.Value ? 0 :(decimal)reader["BULevelSkill"]
                     };

                     businessUnitSkillModels.Add(buSkillModel);
                  }
               }
            }


            return businessUnitSkillModels;
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message, ex.InnerException);
         }
      }
   }
}


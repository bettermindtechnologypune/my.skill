using Microsoft.Extensions.Configuration;
using MySqlConnector;
using skill.common.Helper;
using skill.common.Model;
using skill.repository.Entity;
using skill.repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Implementation
{
   public class EmployeeRepositoryImpl : BaseRepository<EmployeeEntity>, IEmployeeRepository
   {
      public EmployeeRepositoryImpl(IConfiguration configuration, ApplicationDBContext applicationDBContext) : base(applicationDBContext, configuration)
      {

      }

      public PagedResult<EmployeeEntity> GetListByManagerId(Guid managerId, int pageNumber, int pageSize, string searchText)
      {
         try
         {
            //searchText = searchText.ToLower();
            List<EmployeeEntity> employeeEntities;
            //var query = _entities.Where(x => x.ManagerId == managerId && (x.FirstName.ToLower().Contains(searchText) || x.LastName.ToLower().Contains(searchText)));

            var query = _entities.Where(x => x.ManagerId == managerId);

            if (pageNumber > 0 && pageSize > 0)
            {
               employeeEntities = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
               employeeEntities = query.ToList();
            }

          
            PagedResult<EmployeeEntity> result = new PagedResult<EmployeeEntity>();
            result.Results = employeeEntities;
            result.RowCount = query.Count();
            result.PageSize = pageSize;
            result.CurrentPage = pageNumber;
            result.PageCount = (int)Math.Ceiling((double)result.RowCount / pageSize);

            return result;
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message, ex.InnerException);
         }
      }

      public EmployeeEntity GetByBUIDAndEmail(Guid buid, string email)
      {
         try
         {
            return _entities.Where(x => x.BUID == buid && x.Email == email).FirstOrDefault();
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message, ex.InnerException);
         }
      }

      public List<EmployeeEntity> GetListByDepartmentId(Guid depatmentId , bool? isManager)
      {
         try
         {
            return _entities.Where(x => x.DepartmentId == depatmentId && x.IsManager == isManager).ToList();
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message, ex.InnerException);
         }
      }

      public override async Task<EmployeeEntity> InsertAsync(EmployeeEntity entity)
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

      public async Task<List<EmployeeSkillMeasurement>> GetEmployeeSkillMeasurement(Guid buid)
      {
         List<EmployeeSkillMeasurement> employeeSkillMeasurements = null;
         try
         {
            string query = @"select OrgEmpId as EmpCode, EmpName, EmpId, count(cal) SkillCount from
                           (select OrgEmpId, concat(e.FirstName, ' ' , e.LastName ) as EmpName, 
                           e.Id EmpId, sum(r.Rating * t.wattage)/100 cal from business_unit bu 
                           inner join skill_db.levelone l1 on l1.BUID = bu.Id and bu.Id = @BUID
                           inner  join skill_db.leveltwo l2 on l2.LevelOneId = l1.Id
                           inner join skill_db.task t on t.levelId = l2.Id
                           inner join skill_db.rating r on r.TaskId = t.Id
                           inner join skill_db.employee e on e.Id = r.EmpId 
                           group by  e.Id, l2.Id having (cal) > 1) temp1 group by EmpId";

            using (var command = new MySqlCommand(query, Connection))
            {
               command.Parameters.AddWithValue("@BUID", buid);

               await Connection.OpenAsync();
               using (var reader = await command.ExecuteReaderAsync())
               {
                  employeeSkillMeasurements = new List<EmployeeSkillMeasurement>();
                  while (reader.Read())
                  {
                     var employeeSkillMeasurement = new EmployeeSkillMeasurement
                     {
                        EmployeeId = (Guid)reader["EmpId"],
                        EmployeeCode = (string)reader["EmpCode"],
                        EmployeeName = (string)reader["EmpName"],
                        SkillCount = Convert.ToInt32(reader["SkillCount"])
                     };

                     employeeSkillMeasurements.Add(employeeSkillMeasurement);
                  }
               }
            }

            return employeeSkillMeasurements;
         }
         catch (Exception ex)
         {
            throw;
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

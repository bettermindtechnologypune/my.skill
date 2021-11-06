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
   public class EmployeeRepositoryImpl : BaseRepository<EmployeeEntity>, IEmployeeRepository
   {
      public EmployeeRepositoryImpl(IConfiguration configuration, ApplicationDBContext applicationDBContext) : base(applicationDBContext, configuration)
      {

      }

      public List<EmployeeEntity> GetListByManagerId(Guid managerId)
      {
         try
         {
            return _entities.Where(x => x.ManagerId == managerId).ToList();
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
   }
}

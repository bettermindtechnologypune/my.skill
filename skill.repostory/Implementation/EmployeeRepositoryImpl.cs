using Microsoft.Extensions.Configuration;
using skill.common.Helper;
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
   }
}

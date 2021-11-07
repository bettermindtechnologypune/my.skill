using skill.common.Helper;
using skill.common.Model;
using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Interface
{
   public interface IEmployeeRepository : IBaseRepositoy<EmployeeEntity>
   {
      PagedResult<EmployeeEntity> GetListByManagerId(Guid managerId, int pageNumber, int pageSize, string searchText);

      EmployeeEntity GetByBUIDAndEmail(Guid buid, string email);

      List<EmployeeEntity> GetListByDepartmentId(Guid depatmentId, bool? isManager);
   }
}

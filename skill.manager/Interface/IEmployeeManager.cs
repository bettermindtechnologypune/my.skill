using skill.common.Helper;
using skill.common.Model;
using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Interface
{
   public interface IEmployeeManager
   {
      Task<EmployeeResource> Create(EmployeeResource resource);

      PagedResult<EmployeeResource> GetListByManagerId(Guid managerId, int pageNumber, int pageSize, string searchText);

      List<EmployeeResource> GetListByDepartmentId(Guid depatmentId);
   }
}

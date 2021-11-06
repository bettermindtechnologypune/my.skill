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
      List<EmployeeEntity> GetListByManagerId(Guid managerId);

      EmployeeEntity GetByBUIDAndEmail(Guid buid, string email);

      List<EmployeeEntity> GetListByDepartmentId(Guid depatmentId);
   }
}

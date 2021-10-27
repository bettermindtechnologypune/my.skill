using skill.common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Interface
{
   public interface IBusinessUnitManager
   {
      Task<BusinessUnitResource> Create(BusinessUnitResource resource);

      Task<BusinessUnitResource> GetByAdminId(Guid adminId);

      Task<BusinessUnitResource> GetById(Guid id);
   }
}

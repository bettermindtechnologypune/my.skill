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
      Task<bool> Create(BusinessUnitResource resource);
   }
}

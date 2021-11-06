﻿using skill.common.Model;
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

      List<EmployeeResource> GetListByManagerId(Guid managerId);
   }
}

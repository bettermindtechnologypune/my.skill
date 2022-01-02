using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.common.Model
{
   public class EmployeeSkillMeasurement
   {
      public string EmployeeCode { get; set; }

      public Guid EmployeeId { get; set; }

      public string EmployeeName { get; set; }

      public int SkillCount { get; set; }
   }
}

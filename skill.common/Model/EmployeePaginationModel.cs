using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.common.Model
{
   public class EmployeePaginationModel
   {
      public int PageSize { get; set; } = 10;

      public int PageNumber { get; set; } = 1;

      public string SearchText { get; set; }
   }
}

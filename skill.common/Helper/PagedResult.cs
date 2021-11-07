using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.common.Helper
{
   public class PagedResult<T> : PagedResultBase where T : class
   {
      public IList<T> Results { get; set; }

      public PagedResult()
      {
         Results = new List<T>();         
      }
   }
}

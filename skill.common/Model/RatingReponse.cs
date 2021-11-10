using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.common.Model
{
   public class RatingReponse
   {
      public Guid TaskId { get; set; }

      public string TaskName { get; set; }

      public int EmpRating { get; set; }

      public Nullable<int> MangerRating { get; set; }
   }
}

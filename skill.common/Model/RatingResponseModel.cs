using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.common.Model
{
   public class RatingResponseModel
   {
      public Guid BUID { get; set; }

      public string BUName { get; set; }

      public Guid LevelOneId { get; set; }

      public string LevelOneName { get; set; }

      public Guid LevelTwoId { get; set; }

      public string LevelTwoName { get; set; }

      public List<RatingReponse> RatingReponseList { get; set; }
     
   }
}

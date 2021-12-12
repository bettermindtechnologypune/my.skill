using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.common.Model
{
   public class LevelTwoSkillModel
   {
      public Guid LevelTwoId { get; set; }

      public string LevelTwoName { get; set; }

      public long SkillLevelZeroCount { get; set; }

      public long SkillLevelOneCount { get; set; }

      public long SkillLevelTwoCount { get; set; }

      public long SkillLevelThreeCount { get; set; }

      public long SkillLevelFourCount { get; set; }
      
      public long SkillLevelFiveCount { get; set; }
   }
}

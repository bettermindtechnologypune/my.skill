using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.common.Model
{
   public class TaskResource
   {
      public Guid Id { get; set; }
   
      public Guid LevelId { get; set; }
     
      public string Name { get; set; }

      public int Wattage { get; set; }
      
   }
}

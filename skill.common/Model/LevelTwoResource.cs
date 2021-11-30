using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.common.Model
{
   public class LevelTwoResource
   {

      public Guid Id { get; set; }

      public Guid LevelOneId { get; set; }

      [Required]     
      [StringLength(50)]
      public string Name { get; set; }

      public bool IsLastLevel { get; set; }
   }
}

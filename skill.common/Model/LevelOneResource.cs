using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.common.Model
{
   public class LevelOneResource
   {
      public Guid Id { get; set; }

      [Required]
      public Guid BUID { get; set; }

      [Required]
      [StringLength(50)]
      public string Name { get; set; }

      public bool IsLastLevel { get; set; }
   }
}

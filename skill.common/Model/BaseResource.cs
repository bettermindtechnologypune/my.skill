using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.common.Model
{
   public class BaseResource
   {
      [Required]
      public Guid Id { get; set; }

      [Required]
      public Guid OrgId { get; set; }
   }
}

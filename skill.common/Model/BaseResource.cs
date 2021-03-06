using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace skill.common.Model
{
   public class BaseResource
   {
      [Required]
      [JsonIgnore]
      public Guid Id { get; set; }

      [Required]
      [JsonIgnore]
      public Guid OrgId { get; set; }
   }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace skill.common.Model
{
   public class DepartmentResource
   {
      [Required]
      [JsonIgnore]
      public Guid Id { get; set; }

      [Required]
      public Guid BusinessUnitId { get; set; }

      [Required]
      public string Name { get; set; }

      public Guid ManagerId { get; set; }


   }
}

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
      
      [JsonIgnore]
      public Guid Id { get; set; }
      
      public Guid BusinessUnitId { get; set; }

      [Required]
      public string Name { get; set; }

      public Guid ManagerId { get; set; }


   }
}

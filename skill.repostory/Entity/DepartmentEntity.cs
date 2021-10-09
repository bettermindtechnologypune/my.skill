using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Entity
{
   public class DepartmentEntity : IBaseEntity
   {
      [Required]
      public Guid Id { get ; set; }     

      [Required]
      public Guid BusinessUnitId { get; set; }

      [Required]
      public string Name { get; set; }
      [Required]
      [StringLength(50)]
      public string CreatedBy { get; set; }

      [StringLength(50)]
      public string ModifiedBy { get; set; }

      [Required]
      [DataType(DataType.DateTime)]
      public DateTime CreatedDate { get; set; }

      [DataType(DataType.DateTime)]
      public DateTime ModifiedDate { get; set; }
   }
}

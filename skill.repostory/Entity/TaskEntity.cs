using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Entity
{
   [Table("Task")]
   public class TaskEntity : IBaseEntity
   {
      [Required]
      public Guid Id { get; set; }

      [Required]
      public Guid LevelId { get; set; }

      [Required]
      [StringLength(50)]
      public string Name { get; set; }

      public int Wattage { get; set; }

      [DataType(DataType.DateTime)]
      public DateTime CreatedDate { get; set; }

      [Required]
      public Guid CreatedBy { get; set; }

      [DataType(DataType.DateTime)]
      public DateTime ModifiedDate { get; set; }

      public Guid ModifiedBy { get; set; }
   }
}

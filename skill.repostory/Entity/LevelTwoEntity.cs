using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Entity
{
   [Table("LevelTwo")]
   public class LevelTwoEntity : IBaseEntity
   {

      public Guid Id { get; set; }

      [Required]
      public Guid LevelOneId { get; set; }

      [Required]
      [StringLength(50)]
      public string Name { get; set; }

      public bool IsLastLevel { get; set; }

      [StringLength(50)]
      public string CreatedBy { get; set; }

      [StringLength(50)]
      public string? ModifiedBy { get; set; }

      [DataType(DataType.DateTime)]
      public DateTime CreatedDate { get; set; }

      [DataType(DataType.DateTime)]
      public DateTime? ModifiedDate { get; set; }
   }
}

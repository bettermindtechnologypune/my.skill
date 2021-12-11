using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Entity
{
   [Table("Rating")]
   public class RatingEntity : IBaseEntity
   {
      [Required]
      public Guid Id { get; set; }

      [Required]
      public string Name { get; set; }

      [Required]
      public Guid TaskId { get; set; }

      [Required]
      public Guid EmpId { get; set; }

      public int Rating { get; set; }

      public int ManagerRating { get; set; }

      [DataType(DataType.DateTime)]
      public DateTime CreatedDate { get; set; }

      [Required]
      public Guid CreatedBy { get; set; }

      [DataType(DataType.DateTime)]
      public DateTime ModifiedDate { get; set; }

      public Guid ModifiedBy { get; set; }
   }
}

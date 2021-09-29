using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Entity
{
  public class BaseEntity : IBaseEntity
   {
      public Guid Id { get; set; }

      public Guid OrgId { get; set; }

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

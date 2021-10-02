using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Entity
{
   [Table("business_unit")]
   public class BusinessUnitEntity :BaseEntity
   {
      [StringLength(50)]
      public string Name { get; set; }

      [StringLength(50)]
      public string Email { get; set; }

      [StringLength(100)]
      public string CompanyAddress { get; set; }

      [StringLength(50)]
      [Required]
      public string City { get; set; }

      [StringLength(50)]
      [Required]
      public string State { get; set; }

      [StringLength(20)]
      [Required]
      public string PostalCode { get; set; }

      [StringLength(20)]
      [Required]
      public string ContactNumber { get; set; }

      [StringLength(50)]
      [Required]
      public string WebSite { get; set; }

      [Required]
      public Guid AdminId { get; set; }
   }
}

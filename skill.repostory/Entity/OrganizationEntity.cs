using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace skill.repository.Entity
{
   [Table("Organization")]
   public class OrganizationEntity : IBaseEntity
   {
      public Guid  Id { get; set; }

      [StringLength(50)]
      public string Name { get; set; }

      [StringLength(50)]
      public string Email { get; set; }

      [StringLength(50)]
      public string? BillingEmail { get; set; }

      [StringLength(50)]
      public string CreatedBy { get; set; }

      [StringLength(50)]
      public string? ModifiedBy { get; set; }

      [DataType(DataType.DateTime)]
      public DateTime CreateDate { get; set; }

      [DataType(DataType.DateTime)]
      public DateTime? ModifiedDate { get; set; }

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
      public bool HasMultipleBU { get; set; }
   }
}

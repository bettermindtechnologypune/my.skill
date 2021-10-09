using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace skill.common.Model
{
   public class OrganizationResource
   {
      public Guid Id { get; set; }

      [StringLength(50)]
      [Required]
      public string Name { get; set; }

      [StringLength(50)]
      [Required]
      public string Email { get; set; }

      [StringLength(50)]
      [Required]
      public string BillingEmail { get; set; }     

      [StringLength(100)]
      [Required]
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
      public string WebSite { get; set; }
    
      public bool HasMultipleBU { get; set; }


   }
}

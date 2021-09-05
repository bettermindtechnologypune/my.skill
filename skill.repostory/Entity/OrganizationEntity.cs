using System;
using System.Collections.Generic;
using System.Text;

namespace skill.repostory.Entity
{
   public class OrganizationEntity
   {
      public Guid Id { get; set; }

      public string Name { get; set; }

      public string Email { get; set; }

      public string BillingEmail { get; set; }

      public Guid CreatedBy { get; set; }

      public Guid ModifiedBy { get; set; }

      public DateTime CreateDate { get; set; }

      public DateTime ModifiedDate { get; set; }
          
   }
}

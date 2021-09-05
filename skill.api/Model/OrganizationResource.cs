using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skills.Model
{
   public class OrganizationResource
   {
      public Guid Id { get; set; }

      public string Name { get; set; }

      public string Email { get; set; }    
      
      public string BillingEmail { get; set; }

   }
}

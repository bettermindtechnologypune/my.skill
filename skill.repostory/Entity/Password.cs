using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repostory.Entity
{
   public class Password
   {
      public Guid Id { get; set; }
      public Guid  UserId{get;set;}
      public DateTime ChangedAt { get; set; }
      public string password { get; set; }
   }
}

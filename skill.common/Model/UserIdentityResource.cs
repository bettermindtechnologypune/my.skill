using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skills.common.Model
{
   public class UserIdentityResource
   {
      public Guid Id { get; set; }
      public string Name { get; set; }
      public string UserCode { get; set; }
      public string PhoneNumber { get; set; }
      public string Email { get; set; }
      public string Password { get; set; }
      public string FullName { get; set; }
      public bool IsDeleted { get; set; }
      public int FailedLoginCount { get; set; }
      public DateTime LastLoginAttemptAt { get; set; }
      public DateTime LastSuccessfulLoginAt { get; set; }
      public DateTime PasswordChangedAt { get; set; }
      public DateTime CreatedAt { get; set; }
      public DateTime ModifiedAt { get; set; }
      public bool IsLoginLocked {get;set;}
   }
}

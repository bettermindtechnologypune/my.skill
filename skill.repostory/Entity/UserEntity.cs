using skill.common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Entity
{
   public class UserEntity : IBaseEntity
   {
      public Guid Id { get; set; }
      public Guid OrgId { get; set; }

      public bool IsDeleted { get; set; }

      public DateTime CreatedTime { get; set; }

      public Guid Createdby { get; set; }

      public DateTime ModifiedTime { get; set; }

      public Guid Modifiedby { get; set; }

      public Guid IdentityUserId { get; set; }

      public string UserName { get; set; }

      public string Name { get; set; }

      public string Contact { get; set; }

      public string Email { get; set; }

      public string UserCode { get; set; }

      public bool IsOrgAdmin { get; set; }

      public UserType UserType { get; set; }
     
   }
}

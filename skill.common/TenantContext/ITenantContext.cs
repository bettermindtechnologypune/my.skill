using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.common.TenantContext
{
   public interface ITenantContext
   {
      Guid OrgId { get; }

      Guid UserId { get; }
      Guid BUId { get; }
   }
}

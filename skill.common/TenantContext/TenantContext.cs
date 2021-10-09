using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.common.TenantContext
{
   public class TenantContext : ITenantContext
   {
      IHttpContextAccessor _httpContext;
      public TenantContext(IHttpContextAccessor httpContext)
      {
         _httpContext = httpContext;
      }
      public Guid OrgId
      {
         get
         {
            return new Guid(_httpContext.HttpContext.Items["Org_Id"].ToString());
         }
      }


      public Guid UserId
      {
         get
         {
            return new Guid(_httpContext.HttpContext.Items["UserId"].ToString());
         }
      }


      public Guid BUId
      {
         get
         {
            return _httpContext.HttpContext.Items["BUID"] != null? new Guid(_httpContext.HttpContext.Items["BUID"].ToString()) : Guid.Empty;
         }
      }
   }
}

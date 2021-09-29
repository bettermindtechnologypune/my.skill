using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using skill.common.Enum;
using skill.common.TenantContext;
using skill.manager.Interface;
using skill.repository.Entity;
using skill.common.Model;
using skill.Filters;


namespace skill.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class OrganizationController : ControllerBase
   {
      IOrganizationManager _organizationManager;
      private readonly ILogger<OrganizationController> _logger;

      ITenantContext _tenantContext;
      public OrganizationController(IOrganizationManager organizationManager, ILogger<OrganizationController> logger, ITenantContext tenantContext)
      {
         _organizationManager = organizationManager;
         _logger = logger;
         _tenantContext = tenantContext;
      }

      Guid OrgId
      {
         get
         {
            return _tenantContext == null? Guid.Empty : _tenantContext.OrgId;
         }
      }
     

      [Authorize]
      [HttpGet(nameof(GetResult))]
      public IActionResult GetResult()
      {
         return Ok("API Validated");
      }

      [Authorize(UserType.Super_Admin)]
      [HttpPost(nameof(Create))]
      [Produces("application/json", "application/xml")]
      public async Task<IActionResult> Create([FromBody] OrganizationResource resource)
      {
         try
         {           
            if (resource != null)
            {
               var result = await _organizationManager.CreateOrganization(resource);
               return Ok(result);
            }
            else
            {
               return StatusCode(400, "Organization is null");
            }
         }
         catch(Exception ex)
         {
            _logger.LogError(ex.Message, ex.InnerException);
            return StatusCode(500, ex.Message);
         }
      }
   }
}

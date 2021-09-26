using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using skill.manager.Interface;
using skill.repository.Entity;
using skills.Filters;


namespace skills.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class OrganizationController : ControllerBase
   {
      IOrganizationManager _organizationManager;
      public OrganizationController(IOrganizationManager organizationManager)
      {
         _organizationManager = organizationManager;
      }
      // GET: api/Organization
      [HttpGet]
      public IEnumerable<string> Get()
      {
         return new string[] { "value1", "value2" };
      }

      // GET: api/Organization/5
      [HttpGet("{id}", Name = "Get")]
      public string Get(int id)
      {
         return "value";
      }

      // POST: api/Organization
      //[HttpPost]
      //public void Post([FromBody] string value)
      //{
      //}

      // PUT: api/Organization/5
      [HttpPut("{id}")]
      public void Put(int id, [FromBody] string value)
      {
      }

      // DELETE: api/ApiWithActions/5
      [HttpDelete("{id}")]
      public void Delete(int id)
      {
      }

      [Authorize]
      [HttpGet(nameof(GetResult))]
      public IActionResult GetResult()
      {
         return Ok("API Validated");
      }

      [Authorize]
      [HttpPost(nameof(Create))]
      [Produces("application/json", "application/xml")]
      public async Task<IActionResult> Create([FromBody] OrganizationEntity organizationEntity)
      {
         if(organizationEntity!=null)
         {
            var result = await _organizationManager.CreateOrganization(organizationEntity);
            return Ok(result);
         }
         else
         {
            return Ok("Organization is null");
         }
      }
   }
}

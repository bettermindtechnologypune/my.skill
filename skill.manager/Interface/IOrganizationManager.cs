using skill.repository.Entity;
using skill.common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Interface
{
   public interface IOrganizationManager
   {
      Task<OrganizationResource> CreateOrganization(OrganizationResource resource);

      Task<List<BusinessUnitSkillModel>> GetBUSkillLevel();
   }
}

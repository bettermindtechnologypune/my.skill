using skill.common.Model;
using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace skill.repository.Interface
{
   public interface IOrganizationRepository : IBaseRepositoy<OrganizationEntity>
   {
      Task<List<BusinessUnitSkillModel>> GetBUSkillLevel(Guid OrgId);
   }
}

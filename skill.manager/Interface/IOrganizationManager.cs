using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Interface
{
   public interface IOrganizationManager
   {
      Task<bool> CreateOrganization(OrganizationEntity entity);
   }
}

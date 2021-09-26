using skill.manager.Interface;
using skill.repository.Entity;
using skill.repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Implementation
{
   public class OrganizationManagerImpl : IOrganizationManager
   {
      IOrganizationRepository _organizationRepository;
      public OrganizationManagerImpl(IOrganizationRepository organizationRepository)
      {
         _organizationRepository = organizationRepository;
      }
      public async  Task<bool> CreateOrganization(OrganizationEntity entity)
      {

         return await _organizationRepository.InsertAsync(entity);

      }
   }
}

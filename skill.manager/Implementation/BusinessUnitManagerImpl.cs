using skill.common.Model;
using skill.common.TenantContext;
using skill.manager.Interface;
using skill.manager.Mapper;
using skill.repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Implementation
{
   public class BusinessUnitManagerImpl : IBusinessUnitManager
   {
      IBusinessUnitRepository _businessUnitRepository;
      ITenantContext _tenantContext;
      public BusinessUnitManagerImpl(IBusinessUnitRepository businessUnitRepository, ITenantContext tenantContext)
      {        
         _businessUnitRepository = businessUnitRepository;
         _tenantContext = tenantContext;
      }

      Guid OrgId
      {
         get
         {
            return _tenantContext == null ? Guid.Empty : _tenantContext.OrgId;
         }
      }

      Guid UserId
      {
         get
         {
            return _tenantContext == null ? Guid.Empty : _tenantContext.UserId;
         }
      }

      public async Task<bool> Create(BusinessUnitResource resource)
      {
         resource.Id = Guid.NewGuid();
         var entity = BusinessUnitMapper.ToEntity(resource);
         entity.CreatedBy = UserId.ToString();
         entity.OrgId = OrgId;
         entity.CreatedDate = DateTime.UtcNow;
         return await _businessUnitRepository.InsertAsync(entity);

      }
   }
}

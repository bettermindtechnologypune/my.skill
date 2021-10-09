using skill.common.Model;
using skill.common.TenantContext;
using skill.manager.Interface;
using skill.manager.Mapper;
using skill.repository.Entity;
using skill.repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Implementation
{
   public class DepartmentManagerImpl : IDepartmentManager
   {
      IDepartmentRepository _departmentRepository;
      ITenantContext _tenantContext;
      IBusinessUnitRepository _businessUnitRepository;

      public DepartmentManagerImpl(IDepartmentRepository departmentRepository, ITenantContext tenantContext, IBusinessUnitRepository businessUnitRepository)
      {
         _departmentRepository = departmentRepository;
         _tenantContext = tenantContext;
         _businessUnitRepository = businessUnitRepository;
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

      Guid BUID
      {
         get
         {
            return _tenantContext == null ? Guid.Empty : _tenantContext.BUId;
         }
      }

      public async Task<DepartmentResource> Create(DepartmentResource resource)
      {

         resource.Id = Guid.NewGuid();
         resource.BusinessUnitId = BUID;

         var entity = DepartmentMapper.ToEntity(resource);
         entity.CreatedBy = UserId.ToString();
         entity.CreatedDate = DateTime.UtcNow;
         var result = await _departmentRepository.InsertAsync(entity);
         if (result != null)
            return resource;

         return null;
      }

      public List<DepartmentEntity> GetList()
      {
         return _departmentRepository.GetDepartmentListByBUID(BUID);
         
      }
   }
}

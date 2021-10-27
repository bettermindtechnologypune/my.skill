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
   public class LevelOneManagerImpl : ILevelOneManager
   {
      ITenantContext _tenantContext;
      ILevelOneRepository _levelOneRepository;

      public LevelOneManagerImpl(ILevelOneRepository levelOneRepository, ITenantContext tenantContext)
      {
         _levelOneRepository = levelOneRepository;
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

      Guid BUID
      {
         get
         {
            return _tenantContext == null ? Guid.Empty : _tenantContext.BUId;
         }
      }

      public async Task<List<LevelOneResource>> Create(List<LevelOneResource> resources)
      {
         try
         {
            List<LevelOneEntity> entities = new List<LevelOneEntity>();
            foreach (var resource in resources)
            {
               resource.Id = Guid.NewGuid();
               resource.BUID = BUID;
               var entity = LevelOneMapper.ToEntity(resource);
               entity.CreatedBy = UserId.ToString();
               entity.CreatedDate = DateTime.UtcNow;
               entities.Add(entity);
            }

            var result = await _levelOneRepository.BulkInsertAsync(entities);
            if (result != null && result.Any())
               return resources;
         }
         catch(Exception)
         {
            throw;
         }

         return null;        
      }

      public List<LevelOneResource> GetLevelOneListByBUID(Guid buid)
      {
         try
         {            
            var entities = _levelOneRepository.GetLevelOneListByBUID(buid);
            List<LevelOneResource> levelOneResources = null;
            if(entities.Any())
            {
               levelOneResources = new List<LevelOneResource>();
               foreach (var entity in entities)
               {
                  levelOneResources.Add(LevelOneMapper.ToResource(entity));
               }
            }

            return levelOneResources;
         }
         catch(Exception)
         {
            throw;
         }
      }
   }
}

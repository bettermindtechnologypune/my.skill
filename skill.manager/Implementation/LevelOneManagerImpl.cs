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

      public async Task<bool> UpdateAsync(Guid leveloneId, LevelOneResource levelOneResource)
      {
         var existingEntity =await _levelOneRepository.GetAsync(leveloneId);

         if(existingEntity !=null && existingEntity.Id == leveloneId)
         {
            if(levelOneResource.Name != null)
            {
               existingEntity.Name = levelOneResource.Name;
            }

            existingEntity.ModifiedBy = UserId.ToString();
            existingEntity.ModifiedDate = DateTime.UtcNow;

           _levelOneRepository.UpdateAsync(existingEntity);

            return true;
         }

         return false;
      }

      public async Task<List<LevelOneResource>> UpdateListAsync(List<LevelOneResource> resourcesList)
      {
         List<LevelOneEntity> entityList = new List<LevelOneEntity>();

        
         foreach (var resource in resourcesList)
         {
            if (resource.Id != Guid.Empty)
            {
               var existingEnity = await _levelOneRepository.GetAsync(resource.Id);
               existingEnity.ModifiedBy = UserId.ToString();
               existingEnity.ModifiedDate = DateTime.UtcNow;
               existingEnity.Name = resource.Name;
               
               entityList.Add(existingEnity);
            }
            else
            {
               var taskEnitiy = LevelOneMapper.ToEntity(resource);
               taskEnitiy.CreatedBy = UserId.ToString();
               taskEnitiy.CreatedDate = DateTime.UtcNow;
               await _levelOneRepository.InsertAsync(taskEnitiy);
            }
         }

         var result = await _levelOneRepository.UpdateListAsync(entityList);
         if (result.Any())
         {
            return resourcesList;
         }
         return null;
      }
   }
}

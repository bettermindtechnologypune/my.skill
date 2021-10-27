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
   public class LevelTwoManagerImpl : IlevelTwoManager
   {
         ITenantContext _tenantContext;
         ILevelTwoRepository _levelTwoRepository;

         public LevelTwoManagerImpl(ILevelTwoRepository levelTwoRepository, ITenantContext tenantContext)
         {
            _levelTwoRepository = levelTwoRepository;
            _tenantContext = tenantContext;
         }

      Guid UserId
      {
         get
         {
            return _tenantContext == null ? Guid.Empty : _tenantContext.UserId;
         }
      }

      public async Task<List<LevelTwoResource>> Create(List<LevelTwoResource> resources)
      {
         try
         {
            List<LevelTwoEntity> entities = new List<LevelTwoEntity>();
            foreach (var resource in resources)
            {
               resource.Id = Guid.NewGuid();               
               var entity = LevelTwoMapper.ToEntity(resource);
               entity.CreatedBy = UserId.ToString();
               entity.CreatedDate = DateTime.UtcNow;
               entities.Add(entity);
            }

            var result = await _levelTwoRepository.BulkInsertAsync(entities);
            if (result != null && result.Any())
               return resources;
         }
         catch (Exception)
         {
            throw;
         }

         return null;
      }

      public List<LevelTwoResource> GetLevelTwoListByLevelOneId(Guid levelOneId)
      {
         try
         {
            var entities = _levelTwoRepository.GetLevelOneListByLevelOneId(levelOneId);
            List<LevelTwoResource> levelTwoResources = null;
            if (entities.Any())
            {
               levelTwoResources = new List<LevelTwoResource>();
               foreach (var entity in entities)
               {
                  levelTwoResources.Add(LevelTwoMapper.ToResource(entity));
               }
            }

            return levelTwoResources;
         }
         catch (Exception)
         {
            throw;
         }
      }
   }
}

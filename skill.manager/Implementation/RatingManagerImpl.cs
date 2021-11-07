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
   public class RatingManagerImpl : IRatingManager
   {

      ITenantContext _tenantContext;
      IRatingRepository _ratingRepository;

      public RatingManagerImpl(IRatingRepository ratingRepository, ITenantContext tenantContext)
      {
         _ratingRepository = ratingRepository;
         _tenantContext = tenantContext;
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

      public async Task<List<RatingResource>> CreateBulk(List<RatingResource> resources)
      {
         try
         {
            List<RatingEntity> entities = new List<RatingEntity>();
            foreach (var resource in resources)
            {
               resource.Id = Guid.NewGuid();
               var entity = RatingMapper.ToEntity(resource);
               entity.CreatedBy = UserId;
               entity.CreatedDate = DateTime.UtcNow;
               entities.Add(entity);
            }

            var result = await _ratingRepository.BulkInsertAsync(entities);
            if (result != null && result.Any())
               return resources;
         }
         catch (Exception)
         {
            throw;
         }

         return null;
      }

      public Task<List<RatingResponseModel>> GetEmployeeRatingModel(Guid empId)
      {
         return _ratingRepository.GetEmployeeRatingModel(empId, BUID);
      }

      public List<RatingResource> GetListByEmpId(Guid empId)
      {
         try
         {
            var entities = _ratingRepository.GetListByEmpId(empId);
            List<RatingResource> ratingResource = null;
            if (entities.Any())
            {
               ratingResource = new List<RatingResource>();
               foreach (var entity in entities)
               {
                  ratingResource.Add(RatingMapper.ToResource(entity));
               }
            }

            return ratingResource;
         }
         catch (Exception)
         {
            throw;
         }
      }

      public List<RatingResource> GetListByTaskId(Guid taskId)
      {
         try
         {
            var entities = _ratingRepository.GetListByTaskId(taskId);
            List<RatingResource> ratingResource = null;
            if (entities.Any())
            {
               ratingResource = new List<RatingResource>();
               foreach (var entity in entities)
               {
                  ratingResource.Add(RatingMapper.ToResource(entity));
               }
            }

            return ratingResource;
         }
         catch (Exception)
         {
            throw;
         }
      }
   }
}

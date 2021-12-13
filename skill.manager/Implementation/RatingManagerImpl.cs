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
      IEmployeeRepository _employeeRepository;

      public RatingManagerImpl(IRatingRepository ratingRepository, ITenantContext tenantContext, IEmployeeRepository employeeRepository)
      {
         _ratingRepository = ratingRepository;
         _tenantContext = tenantContext;
         _employeeRepository = employeeRepository;
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
            var employee = await _employeeRepository.GetAsync(resources[0].EmpId);
            List<RatingEntity> entities = new List<RatingEntity>();
            string  ratingName = $"{employee.FirstName} {employee.LastName} - {DateTime.UtcNow.Ticks}";
            foreach (var resource in resources)
            {
               resource.Id = Guid.NewGuid();
               var entity = RatingMapper.ToEntity(resource);
               entity.CreatedBy = UserId;
               entity.CreatedDate = DateTime.UtcNow;
               entity.Name = ratingName;
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

      public async Task<RatingResponseModel> GetEmployeeRatingModel(Guid empId, string ratingName)
      {
         return await _ratingRepository.GetEmployeeRatingModel(empId, ratingName, BUID);
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

      public List<RatingNameModel> GetRatingNameByEmpId(Guid empId)
      {
         List<RatingNameModel> ratingNameModels = null;
         var ratingNameList = _ratingRepository.GetListByEmpId(empId).Select(x=>x.Name).Distinct();
         if(ratingNameList.Any())
         {
            ratingNameModels = new List<RatingNameModel>();
            foreach (var ratingName in ratingNameList)
            {
               RatingNameModel ratingNameModel = new RatingNameModel
               {                  
                  Name = ratingName
               };

               ratingNameModels.Add(ratingNameModel);
            }
         }

         return ratingNameModels;

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

      public async Task<List<RatingResource>> UpdateListAsync(List<RatingResource> resourceList)
      {
         List<RatingEntity> entities = new List<RatingEntity>();


         foreach (var resource in resourceList)
         {
            if (resource.Id != Guid.Empty)
            {
               var existingEnity = await _ratingRepository.GetAsync(resource.Id);
               existingEnity.ModifiedBy = UserId;
               existingEnity.ModifiedDate = DateTime.UtcNow;             
               existingEnity.ManagerRating = resource.ManagerRating;                           
               entities.Add(existingEnity);
            }
            else
            {
               throw new Exception("Rating does not Exists");
            }
         }
         
         return resourceList;
      }
   }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using skill.common.Model;
using skill.repository.Entity;
using skill.repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Implementation
{
   public class BusinessUnitRepositoryImpl : BaseRepository<BusinessUnitEntity>, IBusinessUnitRepository
   {
      public BusinessUnitRepositoryImpl(IConfiguration configuration, ApplicationDBContext applicationDBContext) : base(applicationDBContext, configuration)
      {

      }

      public async Task<BusinessUnitEntity> GetByAdminId(Guid adminId)
      {
         return await _entities.SingleOrDefaultAsync(s => s.Id == adminId);
      }

      public async override Task<BusinessUnitEntity> InsertAsync(BusinessUnitEntity entity)
      {
         try
         {
            return await base.InsertAsync(entity);
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message, ex.InnerException);
         }
      }
   }
}

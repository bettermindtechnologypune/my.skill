using Microsoft.Extensions.Configuration;
using skill.repository.Entity;
using skill.repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Implementation
{
   public class DepartmentRepositoryImpl :BaseRepository<DepartmentEntity> , IDepartmentRepository
   {
      public DepartmentRepositoryImpl(IConfiguration configuration, ApplicationDBContext applicationDBContext) : base(applicationDBContext, configuration)
      {

      }

      public List<DepartmentEntity> GetDepartmentListByBUID(Guid BUID)
      {
         return  entities.Where(x => x.BusinessUnitId == BUID).ToList();
      }

      public async override Task<DepartmentEntity> InsertAsync(DepartmentEntity entity)
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

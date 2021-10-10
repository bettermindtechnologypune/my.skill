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
   public class EmployeeRepositoryImpl : BaseRepository<EmployeeEntity>, IEmployeeRepository
   {
      public EmployeeRepositoryImpl(IConfiguration configuration, ApplicationDBContext applicationDBContext) : base(applicationDBContext, configuration)
      {

      }


      public override async Task<EmployeeEntity> InsertAsync(EmployeeEntity entity)
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

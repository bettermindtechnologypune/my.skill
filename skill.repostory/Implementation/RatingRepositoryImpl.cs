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
   public class RatingRepositoryImpl : BaseRepository<RatingEntity>, IRatingRepository
   {
      public RatingRepositoryImpl(IConfiguration configuration, ApplicationDBContext applicationDBContext) : base(applicationDBContext, configuration)
      {

      }

      public List<RatingEntity> GetListByEmpId(Guid empId)
      {
         try
         {
            return _entities.Where(x => x.EmpId == empId).ToList();
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message, ex.InnerException);
         }
      }

      public List<RatingEntity> GetListByTaskId(Guid taskId)
      {
         try
         {
            return _entities.Where(x => x.TaskId == taskId).ToList();
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message, ex.InnerException);
         }
      }
   }
}

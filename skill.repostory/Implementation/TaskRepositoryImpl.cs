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
   public class TaskRepositoryImpl : BaseRepository<TaskEntity>, ITaskRepository
   {
      public TaskRepositoryImpl(IConfiguration configuration, ApplicationDBContext applicationDBContext) : base(applicationDBContext, configuration)
      {

      }

      public List<TaskEntity> GetTaslListByLevelId(Guid levelId)
      {
         try
         {
            return _entities.Where(x => x.LevelId == levelId).ToList();
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message, ex.InnerException);
         }

      }
   }
}

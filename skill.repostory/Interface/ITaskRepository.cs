using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Interface
{
   public interface ITaskRepository : IBaseRepositoy<TaskEntity>
   {
      List<TaskEntity> GetTaslListByLevelId(Guid levelId);
   }
}

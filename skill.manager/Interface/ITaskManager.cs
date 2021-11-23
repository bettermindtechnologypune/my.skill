using skill.common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Interface
{
   public interface ITaskManager
   {
      Task<List<TaskResource>> CreateBulk(List<TaskResource> resources);

      List<TaskResource> GetTaskListByLevelId(Guid levelId);

      Task<bool> Update(Guid taskId, TaskResource taskResource);

      Task<List<TaskResource>> UpdateListAsync(List<TaskResource> taskResources);
   }
}

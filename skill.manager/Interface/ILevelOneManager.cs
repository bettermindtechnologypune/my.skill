using skill.common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Interface
{
   public interface ILevelOneManager
   {
      Task<List<LevelOneResource>> Create(List<LevelOneResource> resources);

      List<LevelOneResource> GetLevelOneListByBUID(Guid buid);

      Task<bool> UpdateAsync(Guid leveloneId, LevelOneResource levelOneResource);

      Task<List<LevelOneResource>> UpdateListAsync(List<LevelOneResource> resourcesList);
   }
}

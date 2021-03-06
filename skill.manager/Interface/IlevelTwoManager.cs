using skill.common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Interface
{
   public interface IlevelTwoManager
   {
      Task<List<LevelTwoResource>> Create(List<LevelTwoResource> resources);

      List<LevelTwoResource> GetLevelTwoListByLevelOneId(Guid levelOneId);

      Task<bool> UpdateAsync(Guid levelTwoId, LevelTwoResource levelTwoResource);

      List<LevelTwoResource> GetLevelOneListByBUID(Guid BUID);

      Task<List<LevelTwoResource>> UpdateListAsync(List<LevelTwoResource> levelTwoResources);

      Task<List<LevelTwoSkillModel>> GetSkillIndexForLevelTwoByLevelOneId(Guid levelOneId);

      Task<List<MultiSkillModelLevelTwo>> GetMultiSkillLevelTwoByLevelOneId(Guid levelOneId);
   }
}

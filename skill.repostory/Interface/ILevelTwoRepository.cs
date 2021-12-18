using skill.common.Model;
using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Interface
{
   public interface ILevelTwoRepository : IBaseRepositoy<LevelTwoEntity>
   {
      List<LevelTwoEntity> GetLevelOneListByLevelOneId(Guid levelOneId);

      List<LevelTwoEntity> GetLevelOneListByBUID(Guid BUID);

      Task<List<LevelTwoSkillModel>> GetSkillIndexForLevelTwoByLevelOneId(Guid levelOneId);

      Task<List<MultiSkillModelLevelTwo>> GetMultiSkillLevelTwoByLevelOneId(Guid levelOneId);
   }
}

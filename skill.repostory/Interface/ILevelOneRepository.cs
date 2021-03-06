using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Interface
{
   public interface ILevelOneRepository : IBaseRepositoy<LevelOneEntity>
   {
      List<LevelOneEntity> GetLevelOneListByBUID(Guid buid);
   }
}

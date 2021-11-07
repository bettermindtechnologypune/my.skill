using skill.common.Model;
using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Interface
{
    public interface IRatingRepository : IBaseRepositoy<RatingEntity>
   {
      List<RatingEntity> GetListByEmpId(Guid empId);

      List<RatingEntity> GetListByTaskId(Guid taskId);


      Task<List<RatingResponseModel>> GetEmployeeRatingModel(Guid empId, Guid BUID);
   }
}

using skill.common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.manager.Interface
{
   public interface IRatingManager
   {
      Task<List<RatingResource>> CreateBulk(List<RatingResource> resources);
      List<RatingResource> GetListByEmpId(Guid empId);

      List<RatingResource> GetListByTaskId(Guid taskId);

      Task<RatingResponseModel> GetEmployeeRatingModel(Guid empId, string ratingName);

      List<RatingNameModel> GetRatingNameByEmpId(Guid empId);
   }
}

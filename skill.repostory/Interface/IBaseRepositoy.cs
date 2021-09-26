using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Interface
{
   public interface IBaseRepositoy<T> where T : class
   {
      IEnumerable<T> GetAll();
      T GetAsync(Guid id);
      Task<Boolean> InsertAsync(T entity);
      void UpdateAsync(T entity);
      void DeleteAsync(T entity);  
   
   }
}

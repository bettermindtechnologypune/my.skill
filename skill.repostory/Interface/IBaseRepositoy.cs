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
      Task<T> GetAsync(Guid id);
      Task<T> InsertAsync(T entity);

      Task<List<T>> BulkInsertAsync(List<T> entity);
      void UpdateAsync(T entity);
      void DeleteAsync(T entity);  
   
   }
}

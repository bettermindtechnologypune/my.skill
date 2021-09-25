using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using skill.repository.Entity;
using skill.repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Implementation
{
   public class BaseRepository<T> : ICommonRepository, IBaseRepositoy<T> where T : class, IBaseEntity 
   {
      protected readonly ApplicationDBContext _context;
      protected DbSet<T> entities;
      string errorMessage = string.Empty;

      protected MySqlConnection Connection;
      private IConfiguration configuration;
      private readonly IConfiguration _configuration;
    

      public void Dispose()
      {
         throw new NotImplementedException();
      }

      public BaseRepository(ApplicationDBContext context, IConfiguration configuration)
      {
         _configuration = configuration;
           Connection = new MySqlConnection(configuration["ConnectionStrings:Default"]);
         _context = context;
         entities = _context.Set<T>();
      }


      public void DeleteAsync(T entity)
      {
         if (entity == null)
         {
            throw new ArgumentNullException("entity");
         }
         entities.Remove(entity);
         _context.SaveChanges();
      }

      public IEnumerable<T> GetAll()
      {
         return entities.AsEnumerable();
      }

      public T GetAsync(Guid id)
      {
         return entities.SingleOrDefault(s => s.Id == id);
      }

      public virtual void InsertAsync(T entity)
      {
         if (entity == null)
         {
            throw new ArgumentNullException("entity");
         }
         entities.Add(entity);
         _context.SaveChanges();
      }

      public virtual void UpdateAsync(T entity)
      {
         if (entity == null)
         {
            throw new ArgumentNullException("entity");
         }
         _context.SaveChanges();
      }
   }
}

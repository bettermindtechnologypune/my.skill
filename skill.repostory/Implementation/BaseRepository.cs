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
         Connection.Dispose();
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

      public async Task<T> GetAsync(Guid id)
      {
         return await entities.SingleOrDefaultAsync(s => s.Id == id);
      }

      public virtual async Task<T> InsertAsync(T entity)
      {
         if (entity == null)
         {
            throw new ArgumentNullException("entity");
         }       
         entities.Add(entity);
         await _context.SaveChangesAsync();

         return entity;
      }

      public virtual void UpdateAsync(T entity)
      {
         if (entity == null)
         {
            throw new ArgumentNullException("entity");
         }
         _context.SaveChanges();
      }

      public async Task<List<T>> BulkInsertAsync(List<T> entities)
      {
         if (!entities.Any())
         {
            throw new ArgumentNullException("entity");
         }
         entities.AddRange(entities);
         await _context.SaveChangesAsync();

         return entities;
      }
   }
}

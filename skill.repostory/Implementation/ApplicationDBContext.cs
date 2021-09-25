using Microsoft.EntityFrameworkCore;
using skill.repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Implementation
{
   public class ApplicationDBContext : DbContext
   {
      // This is the run time configuration of 
      public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
          : base(options)
      { }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<OrganizationEntity>();
         modelBuilder.Entity<UserIdentityEntity>();
         modelBuilder.Entity<UserEntity>().ToTable("user_identity");

         base.OnModelCreating(modelBuilder);
         
      }
   }
}

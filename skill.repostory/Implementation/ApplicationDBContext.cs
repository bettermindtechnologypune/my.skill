﻿using Microsoft.EntityFrameworkCore;
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
      public DbSet<OrganizationEntity> Orgnization { get; set; }
      //public DbSet<UserEntity> Orders { get; set; }
      public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
          : base(options)
      {
        
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {

         //Configure default schema
         //modelBuilder.HasDefaultSchema("skill_db");

         modelBuilder.Entity<OrganizationEntity>();

         

         modelBuilder.Entity<GlobalConfig>();

         //modelBuilder.Entity<UserIdentityEntity>();
         //modelBuilder.Entity<UserEntity>();


         // Configure Primary Keys  
         modelBuilder.Entity<OrganizationEntity>().HasKey(ug => ug.Id).HasName("Idx_PK_Organization");

         // Configure indexes 
         modelBuilder.Entity<OrganizationEntity>().HasIndex(p => p.Email).IsUnique().HasDatabaseName("Idx_UQ_OrgEmail");


         // Configure columns  
         modelBuilder.Entity<OrganizationEntity>().Property(ug => ug.Email).IsRequired();
         modelBuilder.Entity<OrganizationEntity>().Property(ug => ug.Name).IsRequired();

       



                  base.OnModelCreating(modelBuilder);
         
      }
   }
}

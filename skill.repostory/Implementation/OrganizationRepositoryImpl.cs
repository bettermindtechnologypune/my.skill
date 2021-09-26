using Microsoft.Extensions.Configuration;
using skill.repository.Entity;
using skill.repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Implementation
{
   public class OrganizationRepositoryImpl :BaseRepository<OrganizationEntity> , IOrganizationRepository
   {
     
      public OrganizationRepositoryImpl(IConfiguration configuration, ApplicationDBContext applicationDBContext) :base(applicationDBContext,configuration)
      { 

      }

      public override async Task<bool> InsertAsync(OrganizationEntity entity)
      {         
         return await base.InsertAsync(entity);        
      }
      public void Get()
      {
         //Connection.OpenAsync();
      }
   }
}


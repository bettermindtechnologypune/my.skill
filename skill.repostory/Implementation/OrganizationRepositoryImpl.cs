using Microsoft.Extensions.Configuration;
using skill.repository.Entity;
using skill.repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace skill.repository.Implementation
{
   public class OrganizationRepositoryImpl : CommonRepostioryImpl, IOrganizationRepository
   {
      public OrganizationRepositoryImpl(IConfiguration configuration) :base(configuration)
      { 

      }
      public void Get()
      {
         Connection.OpenAsync();
      }
   }
}


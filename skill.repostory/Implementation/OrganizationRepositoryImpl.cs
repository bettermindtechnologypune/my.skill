using Microsoft.Extensions.Configuration;
using skill.repostory.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace skill.repostory.Implementation
{
   public class OrganizationRepositoryImpl : CommonRepostioryImpl, IOrganizationRepository
   {
      public OrganizationRepositoryImpl(IConfiguration configuration) :base(configuration["ConnectionStrings:Default"])
      { 

      }
      public void Get()
      {
         Connection.OpenAsync();
      }
   }
}


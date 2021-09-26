using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using skill.repository.Entity;
using skill.repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace skill.repository.Implementation
{
   public class CommonRepostioryImpl : ICommonRepository
   {
      public MySqlConnection Connection;
      private readonly IConfiguration _configuration;
      public CommonRepostioryImpl(IConfiguration configuration)
      {
         _configuration = configuration;
         Connection = new MySqlConnection(configuration["ConnectionStrings:Default"]);         
      }

      public void Dispose()
      {
         Connection.Dispose();
      }
   }
}

using MySql.Data.MySqlClient;
using skill.repostory.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace skill.repostory.Implementation
{
   public class CommonRepostioryImpl : ICommonRepository
   {
      public MySqlConnection Connection;
      public CommonRepostioryImpl(string connectionString)
      {
         Connection = new MySqlConnection(connectionString);         
      }
    
      public void Dispose()
      {
         throw new NotImplementedException();
      }
   }
}

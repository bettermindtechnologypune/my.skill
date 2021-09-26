using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using skill.repository.Entity;
using skill.repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skill.repository.Implementation
{
   public class EmailSettingRepository : CommonRepostioryImpl, IEmailSettingsRepository
   {
      public EmailSettingRepository(IConfiguration configuration) : base(configuration)
      {

      }
      public async Task<EmailSettingEntity> GetEmailSetting()
      {
         EmailSettingEntity entity = null;
         string query = "select value from global_config where CATAGORY = 'SMTP SETTINGS' ";
         using (var command = new MySqlCommand(query, Connection))
         {
            await Connection.OpenAsync();
            DataTable dt = new DataTable();
            using (var reader = await command.ExecuteReaderAsync())
            {
               dt.Load(reader);
               entity = new EmailSettingEntity
               {
                     Port =Convert.ToInt32((string) dt.Rows[0].ItemArray[0]),
                     Host = (string)dt.Rows[1].ItemArray[0],
                     Mail = (string)dt.Rows[2].ItemArray[0],
                     Password = (string)dt.Rows[3].ItemArray[0],
                     DisplayName = (string)dt.Rows[4].ItemArray[0]
               };             
            }
         }

         return entity;

      }

      public async Task<string> GetSymmetricKey()
      {
         try
         {
            string query = "select value from global_config where CATAGORY = 'SYMMETRIC_KEY' ";
            string key = null;
            using (var command = new MySqlCommand(query, Connection))
            {
               await Connection.OpenAsync();
               using (var reader = await command.ExecuteReaderAsync())
               {
                  while (await reader.ReadAsync())
                  {
                     key = (string)reader["Value"];
                  }
               }
            }

            return key;
         }
         finally
         {
            if (Connection.State == ConnectionState.Open)
            {
               Connection.Close();
            }
         }
      }

   }
}

using Microsoft.Extensions.Configuration;

using skill.repository.Entity;
using skill.repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using skill.common.Enum;

namespace skill.repository.Implementation
{
   public class UserIdentityRepositoryImpl : IUserIdentityRepository, IDisposable
   {
      public MySqlConnection Connection;
      private readonly IConfiguration _configuration;
      public UserIdentityRepositoryImpl(IConfiguration configuration)
      {
         _configuration = configuration;
         Connection = new MySqlConnection(_configuration["ConnectionStrings:Identity"]);
      }
      public void Dispose()
      {
         throw new NotImplementedException();
      }

      public async Task CreateUserIdentity(UserIdentityEntity userIdentityEntity)
      {
         try
         {
            string query = @"INSERT INTO user_identity(`Id`,`NAME`,`PHONE_NUMBER`,`EMAIL`,`PASSWORD`,`IS_DELETED`,`FAILED_LOGIN_COUNT`,
                        `LAST_LOGIN_ATTEMPT_AT`,`LAST_SUCCESSFUL_LOGIN_AT`, `PASSWORD_CHANGED_AT`,`CREATED_AT`,`MODIFIED_AT`,
                        `IS_LOGIN_LOCKED`,`FULL_NAME`,`ORG_ID`,`IS_ORG_ADMIN`,`USER_TYPE`)
                     VALUES(@Id, @Name, @PhoneNumber, @Email, @Password, @IsDeleted, @FailedLoginCount, @LastLoginAttemptAt,
                              @LastSuccessfulLoginAt, @PasswordChangedAt, @CreatedAt, @ModifiedAt, @IsloginLocked, @FullName , @OrgId, @IsOrgAdmin, @UserType)";

            using (var command = new MySqlCommand(query, Connection))
            {
               command.Parameters.AddWithValue("@Id", userIdentityEntity.Id);
               command.Parameters.AddWithValue("@PhoneNumber", userIdentityEntity.PhoneNumber);
               command.Parameters.AddWithValue("@Email", userIdentityEntity.Email);
               command.Parameters.AddWithValue("@Password", userIdentityEntity.Password);
               command.Parameters.AddWithValue("@IsDeleted", userIdentityEntity.IsDeleted);
               command.Parameters.AddWithValue("@FailedLoginCount", userIdentityEntity.FailedLoginCount);
               command.Parameters.AddWithValue("@LastLoginAttemptAt", userIdentityEntity.LastLoginAttemptAt);
               command.Parameters.AddWithValue("@LastSuccessfulLoginAt", userIdentityEntity.LastSuccessfulLoginAt);
               command.Parameters.AddWithValue("@Name", userIdentityEntity.Name);
               command.Parameters.AddWithValue("@PasswordChangedAt", userIdentityEntity.PasswordChangedAt);
               command.Parameters.AddWithValue("@CreatedAt", userIdentityEntity.CreatedAt);
               command.Parameters.AddWithValue("@ModifiedAt", userIdentityEntity.ModifiedAt);
               command.Parameters.AddWithValue("@IsloginLocked", userIdentityEntity.IsLoginLocked);
               command.Parameters.AddWithValue("@FullName", userIdentityEntity.FullName);
               command.Parameters.AddWithValue("@IsOrgAdmin", userIdentityEntity.IsOrgAdmin);
               command.Parameters.AddWithValue("@OrgId", userIdentityEntity.OrgId);
               command.Parameters.AddWithValue("@UserType", userIdentityEntity.UserType);

               await Connection.OpenAsync();

               command.ExecuteNonQuery();
            }
         }
         catch(Exception ex)
         {
            throw;
         }
         finally
         {
               if(Connection.State == ConnectionState.Open)
               {
                  Connection.Close();
               }
         }

      }

      public async Task<UserIdentityEntity> GetUserIdentityByEmail(string email, string password = null)
      {
         try
         {
            string where = null;
            if(password!=null)
            {
               where = " AND PASSWORD = @password ";
            }
            UserIdentityEntity userIdentityEntity = null;
            string query = $"select * from user_identity where Email = @email {where}; ";
            using (var command = new MySqlCommand(query, Connection))
            {
               command.Parameters.AddWithValue("@email", email);
               command.Parameters.AddWithValue("@password", password);
               await Connection.OpenAsync();
               using (var reader = await command.ExecuteReaderAsync())
               {
                  while (reader.Read())
                  {
                     userIdentityEntity = new UserIdentityEntity
                     {
                       CreatedAt = (DateTime)reader["CREATED_AT"],
                       Email = (string)reader["EMAIL"],
                       FailedLoginCount = (int)reader["FAILED_LOGIN_COUNT"],
                       FullName = (string)reader["FULL_NAME"],
                       //IsDeleted = Convert.ToBoolean((byte)reader["IS_DELETED"]),
                       //IsLoginLocked = Convert.ToBoolean((byte)reader["IS_LOGIN_LOCKED"]),
                       LastLoginAttemptAt = (DateTime)reader["LAST_LOGIN_ATTEMPT_AT"],
                       LastSuccessfulLoginAt = (DateTime)reader["LAST_SUCCESSFUL_LOGIN_AT"],
                       ModifiedAt = (DateTime)reader["MODIFIED_AT"],
                       Name = (string)reader["NAME"],
                       Password = (string)reader["PASSWORD"],
                       PasswordChangedAt = (DateTime)reader["PASSWORD_CHANGED_AT"],
                       PhoneNumber = reader["PHONE_NUMBER"] == DBNull.Value ? null:(string)reader["PHONE_NUMBER"],
                       Id = new Guid((string)reader["Id"]),
                       OrgId = new Guid((string)reader["ORG_ID"]),
                       //IsOrgAdmin = Convert.ToBoolean((byte)reader["IS_ORG_ADMIN"]),
                       UserType = (UserType)reader["USER_TYPE"]
                     };

                  }
               }
            }

            return userIdentityEntity;

         }
         catch (Exception ex)
         {
            throw;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using skill.manager.Implementation;
using skill.manager.Interface;
using skill.repository.Interface;
using skill.repository.Implementation;
using skill.repository.Interface;
using skills.AuthProvider;
using skills.Middleware;


namespace skills
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddControllers();

         //string mySqlConnectionStr = Configuration.GetConnectionString("ConnectionStrings:DBContext");
         string mySqlConnectionStr = "Data Source=DESKTOP-CA5OS4F; database=skills_db; user=root; password=root; Persist Security Info=False; Connect Timeout=300";
         services.AddDbContextPool<ApplicationDBContext>(options => options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr)));

         // Register the Swagger generator, defining 1 or more Swagger documents  
         //services.AddSwaggerGen();
         #region Swagger Configuration
         services.AddSwaggerGen(swagger =>
         {
            //This is to generate the Default UI of Swagger Documentation
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
               Version = "v1",
               Title = "JWT Token Authentication API",
               Description = "ASP.NET Core 5.0 Web API"
            });
            // To Enable authorization using Swagger (JWT)
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
               Name = "Authorization",
               Type = SecuritySchemeType.ApiKey,
               Scheme = "Bearer",
               BearerFormat = "JWT",
               In = ParameterLocation.Header,
               Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
         });
         #endregion
         services.AddTransient<ICommonRepository>(_ => new CommonRepostioryImpl(Configuration));

        
         services.AddScoped<IOrganizationManager, OrganizationManagerImpl>();
         services.AddScoped<IUserIdentityManager, UserIdentityManagerImpl>();
         services.AddScoped<IUserIdentityManager, UserIdentityManagerImpl>();
         services.AddScoped<IEmailManager, EmailManagerImpl>();

         services.AddScoped<IAuth, Auth>();
         
         services.AddScoped(typeof(IBaseRepositoy<>), typeof(BaseRepository<>));

         //Repostories Injection//
         services.AddScoped<ICommonRepository, CommonRepostioryImpl>();
         services.AddScoped<IOrganizationRepository, OrganizationRepositoryImpl>();
         services.AddScoped<IEmailSettingsRepository, EmailSettingRepository>();
         services.AddScoped<IUserIdentityRepository, UserIdentityRepositoryImpl>();
         services.AddScoped<IUserRepository, UserRepositoryImpl>();

         services.AddScoped<IStartupTaskManager, StartupTaskManagerImpl>();

         services.AddCors();

         services.AddCors(options =>
         {
            options.AddDefaultPolicy(
                builder =>
                {
                   builder.WithOrigins("https://localhost:19006s", "http://localhost:4200")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                });
         });

         #region Authentication
         services.AddAuthentication(option =>
         {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

         }).AddJwtBearer(options =>
         {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = false,
               ValidateIssuerSigningKey = true,
               ValidIssuer = Configuration["Jwt:Issuer"],
               ValidAudience = Configuration["Jwt:Audience"],
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) //Configuration["JwtToken:SecretKey"]
            };
         });
         #endregion
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }

         app.UseHttpsRedirection();
         app.UseMiddleware<AuthenticationMiddleware>();


         app.UseRouting();

         app.UseAuthentication();

         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });

         // Enable middleware to serve generated Swagger as a JSON endpoint.  
         // Enable middleware to serve generated Swagger as a JSON endpoint.  
         app.UseSwagger(c =>
         {
            c.SerializeAsV2 = true;
         });

         // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),  
         // specifying the Swagger JSON endpoint.  
         app.UseSwaggerUI(c =>
         {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = string.Empty;
         });

         app.Use(async (context, next) =>
         {
            await next.Invoke();
         });
      }
   }
}

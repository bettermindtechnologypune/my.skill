using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using skill.manager.Interface;

namespace skill
{
   public class Program
   {
      public static void Main(string[] args)
      {
         //CreateHostBuilder(args).Build().Run();
         var host = CreateHostBuilder(args).Build();
         // Resolve the StartupTasks from the ServiceProvider
         var service = host.Services.CreateScope();
         var startupTasks = service.ServiceProvider.GetService<IStartupTaskManager>();
         startupTasks.Execute();

         host.Run();
      }

      public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureWebHostDefaults(webBuilder =>
              {
                 webBuilder.UseStartup<Startup>();
              });
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using App.Application.Service;
using App.Persistence.Context;
using Clean.Persistence.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Clean.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureServices(services => {
                    //services.AddHostedService(opts =>
                    //{
                    //    var scope = opts.CreateScope();
                    //    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    //    var idcontext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
                    //    var logger = opts.GetRequiredService<ILogger<ResearchQueueService>>();
                    //    return new ResearchQueueService(logger, context,idcontext);
                        
                    //}); 
                    //services.AddHostedService(opts =>
                    //{
                    //    var scope = opts.CreateScope();
                    //    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    //    var idcontext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
                    //    var logger = opts.GetRequiredService<ILogger<AuthorizationQueueService>>();
                    //    return new AuthorizationQueueService(logger, context, idcontext);

                    //});
                    //services.AddHostedService(opts =>
                    //{
                    //    var scope = opts.CreateScope();
                    //    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    //    var idcontext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
                    //    var logger = opts.GetRequiredService<ILogger<PrintQueueService>>();
                    //    return new PrintQueueService(logger, context, idcontext);
                    //});
                });
    }
}

using Coravel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using SynchronizationScheduler.Application;
using SynchronizationScheduler.Infrastructure;
using SynchronizationScheduler.Worker.ScheduleTasks;
using System;

namespace SynchronizationScheduler.Worker
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Starting web host");

                IHost host = CreateHostBuilder(args).Build();
                host.Services.UseScheduler(scheduler =>
                {
                    scheduler
                        .Schedule<PersonSynchronizationScheduler>()
                        .DailyAt(19, 08)
                        .Zoned(TimeZoneInfo.Local);

                    scheduler
                        .Schedule<PostSynchronizationScheduler>()
                        .DailyAt(19, 09)
                        .Zoned(TimeZoneInfo.Local);

                    scheduler
                        .Schedule<CommentSynchronizationScheduler>()
                        .DailyAt(19, 10)
                        .Zoned(TimeZoneInfo.Local);
                });
                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureServices(services =>
                {
                    var configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .Build();

                    services.AddScheduler();
                    services.AddTransient<PersonSynchronizationScheduler>();
                    services.AddTransient<PostSynchronizationScheduler>();
                    services.AddTransient<CommentSynchronizationScheduler>();
                    services.AddApplicationDependency();
                    services.AddInfrastructureDependency(configuration);
                });
    }
}

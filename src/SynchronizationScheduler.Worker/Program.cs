using Coravel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SynchronizationScheduler.Application;
using SynchronizationScheduler.Infrastructure;
using SynchronizationScheduler.Worker.ScheduleTasks;

namespace SynchronizationScheduler.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            host.Services.UseScheduler(scheduler =>
            {
                scheduler
                    .Schedule<PersonSynchronizationScheduler>()
                    .EveryMinute();
            });
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    var configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .Build();

                    services.AddScheduler();
                    services.AddTransient<PersonSynchronizationScheduler>();
                    services.AddApplicationDependency();
                    services.AddInfrastructureDependency(configuration);
                });
    }
}

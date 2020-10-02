using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SynchronizationScheduler.Application.Interfaces;
using SynchronizationScheduler.Infrastructure.ApplicationContext;
using SynchronizationScheduler.Infrastructure.CloudContext;
using SynchronizationScheduler.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SynchronizationScheduler.Infrastructure
{
    /// <summary>
    /// Extension class for adding infrastructure level services.
    /// </summary>
    public static class InfrastructureServiceCollectionExtension
    {
        /// <summary>
        /// Extension method for adding infrastructure services thanks to dependency injection.
        /// </summary>
        /// <param name="serviceCollection">Specifies a contract for a collection of service descriptors.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructureDependency(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ApplicationConnection")));
            serviceCollection.AddDbContext<CloudDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("CloudConnection")));

            serviceCollection.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            serviceCollection.AddScoped<ICloudDbContext>(provider => provider.GetService<CloudDbContext>());

            serviceCollection.AddScoped<IPersonSynchronizationService, PersonSynchronizationService>();
            serviceCollection.AddScoped<IPostSynchronizationService, PostSynchronizationService>();
            serviceCollection.AddScoped<ICommentSynchronizationService, CommentSynchronizationService>();

            return serviceCollection;
        }
    }
}

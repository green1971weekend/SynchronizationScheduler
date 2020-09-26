using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SynchronizationScheduler.Application.Interfaces;
using SynchronizationScheduler.Application.Managers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SynchronizationScheduler.Application
{
    /// <summary>
    /// Extension class for adding application level services.
    /// </summary>
    public static class ApplicationServiceCollectionExtension
    {
        /// <summary>
        /// Extension method for adding application level services thanks to dependency injection.
        /// </summary>
        /// <param name="serviceCollection">Specifies a contract for a collection of service descriptors.</param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationDependency(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
            serviceCollection.AddScoped<IPersonManager, PersonManager>();
            serviceCollection.AddScoped<IPostManager, PostManager>();
            serviceCollection.AddScoped<ICommentManager, CommentManager>();
            serviceCollection.AddScoped<ICloudManager, CloudManager>();

            return serviceCollection;
        }
    }
}

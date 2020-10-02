using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace SynchronizationScheduler.Application.Mapping
{
    /// <summary>
    /// A good way to organize your mapping configurations is with profiles.Configuration inside a profile only applies to maps inside the profile (Configuration applied to the root configuration applies to all maps created).
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// AutoMapper will scan the designated assemblies for classes inheriting from Profile and add them to the configuration
        /// </summary>
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Takes all types which implements IMapFrom interface and creates map foreach instance.
        /// </summary>
        /// <param name="assembly"></param>
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                    ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}

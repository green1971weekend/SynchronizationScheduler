using AutoMapper;

namespace SynchronizationScheduler.Application.Mapping
{
    /// <summary>
    /// Defines generalized configuration for matching types.
    /// </summary>
    /// <typeparam name="T">Source type from which mapping starts.</typeparam>
    public interface IMapFrom<T>
    {
        /// <summary>
        /// Configuration for matching types which implemented through the profile.
        /// </summary>
        /// <param name="profile"></param>
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}

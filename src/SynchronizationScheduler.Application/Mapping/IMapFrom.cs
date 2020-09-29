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
        /// Configuration for matching types which implemented through the profile. CreateMap method takes db entity for typeof(T) and current dto model for GetType().
        /// </summary>
        /// <param name="profile">Mapping profile.</param>
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType()).ReverseMap();
    }
}

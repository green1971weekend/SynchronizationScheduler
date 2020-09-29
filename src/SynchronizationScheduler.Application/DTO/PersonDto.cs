using AutoMapper;
using SynchronizationScheduler.Application.Mapping;
using SynchronizationScheduler.Domain.Models.Application;

namespace SynchronizationScheduler.Application.DTO
{
    /// <summary>
    /// Data transfer object which serves as the shell for interactting with data between database models and program.
    /// </summary>
    public class PersonDto : IMapFrom<Person>
    {
        public int Id { get; set; }

        public int CloudId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        ///<inheritdoc/>
        //public void Mapping(Profile profile)
        //{
        //    // If there is a need to add some specification for mapping - add it here with ForMember method. 
        //    //If not there is no need to add Mapping method here because it implemented in the IMapFrom.
        //    profile.CreateMap<Person, PersonDto>();
        //}
    }
}

using SynchronizationScheduler.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SynchronizationScheduler.Application.Interfaces
{
    /// <summary>
    /// Manager for interacting with the application person data. Works by the Repositry principle pattern but can include more functionality(for example some business logic).
    /// </summary>
    public interface IPersonManager
    {
        /// <summary>
        /// Creates a new person and saving it to the database.
        /// </summary>
        /// <param name="personDto">Data transfer object.</param>
        public Task<int> CreatePersonAsync(PersonDto personDto);

        /// <summary>
        /// Returns an existing person from the database.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public Task<PersonDto> GetPersonAsync(int id);

        /// <summary>
        /// Returns an existing person from the database without tracking this object in the EF cache.
        /// </summary>
        /// <param name="id">Identifier.</param>
        Task<PersonDto> GetPersonWithoutTrackingAsync(int id);

        /// <summary>
        /// Returns an existing person from the database without tracking this object in the EF cache.
        /// </summary>
        /// <param name="id">Cloud Identifier.</param>
        Task<PersonDto> GetPersonWithoutTrackingByCloudIdAsync(int id);

        /// <summary>
        /// Returns a full list of existing persons from the database.
        /// </summary>
        public Task<IEnumerable<PersonDto>> GetPeopleAsync();

        /// <summary>
        /// Returns a full list of existing persons from the database without tracking the objects in the EF cache.
        /// </summary>
        Task<IEnumerable<PersonDto>> GetPeopleWithoutTrackingAsync();

        /// <summary>
        /// Updates an existing person and saving to the database.
        /// </summary>
        /// <param name="personDto">Data transfer object.</param>
        public Task<int> UpdatePersonAsync(PersonDto personDto);

        /// <summary>
        /// Deletes an existing person from the database.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public Task<int> DeletePersonAsync(int id);

        /// <summary>
        /// Deletes an existing person from the database by the cloud id. Solves a problem with different id value between cloud and application.
        /// </summary>
        /// <param name="cloudId">Identifier.</param>
        Task<int> DeletePersonByCloudIdAsync(int cloudId);
    }
}

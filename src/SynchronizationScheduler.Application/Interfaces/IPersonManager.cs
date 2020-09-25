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
        public Task<int> CreatePerson(PersonDto personDto);

        /// <summary>
        /// Returns an existing person from the database.
        /// </summary>
        /// <param name="personDto">Identifier.</param>
        public Task<PersonDto> GetPerson(int id);

        /// <summary>
        /// Returns a full list of existing persons from the database.
        /// </summary>
        public Task<IEnumerable<PersonDto>> GetPeople();

        /// <summary>
        /// Updates an existing person and saving to the database.
        /// </summary>
        /// <param name="personDto">Data transfer object.</param>
        public Task<int> UpdatePerson(PersonDto personDto);

        /// <summary>
        /// Deletes an existing person from the database.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public Task<int> DeletePerson(int id);
    }
}

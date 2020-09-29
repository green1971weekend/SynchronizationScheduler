using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SynchronizationScheduler.Application.Interfaces
{
    /// <summary>
    /// Service for person data synchronization between cloud and application.
    /// </summary>
    public interface IPersonSynchronizationService
    {
        /// <summary>
        /// Adds existing persons in the cloud to the application database.
        /// </summary>
        public Task SynchronizeForAddingPeopleAsync();

        /// <summary>
        /// Deletes existing persons in the cloud to the application database.
        /// </summary>
        public Task SynchronizeForDeletingPeopleAsync();

        /// <summary>
        /// Updates existing persons in the cloud to the application database.
        /// </summary>
        public Task SynchronizeForUpdatingPeopleAsync();
    }
}

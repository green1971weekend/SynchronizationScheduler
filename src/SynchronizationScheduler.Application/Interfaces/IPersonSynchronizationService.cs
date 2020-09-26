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
        /// Synchronizes application person data with the cloud data.
        /// </summary>
        public Task SynchronizeForAddingPeople();
    }
}

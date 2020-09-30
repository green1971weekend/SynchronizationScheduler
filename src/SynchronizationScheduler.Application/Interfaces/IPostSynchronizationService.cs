using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SynchronizationScheduler.Application.Interfaces
{
    /// <summary>
    /// Service for post data synchronization between cloud and application.
    /// </summary>
    public interface IPostSynchronizationService
    {
        /// <summary>
        /// Adds existing posts in the cloud to the application database.
        /// </summary>
        public Task SynchronizeForAddingPostsAsync();

        /// <summary>
        /// Deletes existing posts in the cloud to the application database.
        /// </summary>
        public Task SynchronizeForDeletingPostsAsync();

        /// <summary>
        /// Updates existing posts in the cloud to the application database.
        /// </summary>
        public Task SynchronizeForUpdatingPostsAsync();
    }
}

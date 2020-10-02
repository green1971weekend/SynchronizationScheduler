using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SynchronizationScheduler.Application.Interfaces
{
    /// <summary>
    /// Service for comment data synchronization between cloud and application.
    /// </summary>
    public interface ICommentSynchronizationService
    {
        /// <summary>
        /// Adds existing comments in the cloud to the application database.
        /// </summary>
        public Task SynchronizeForAddingCommentsAsync();

        /// <summary>
        /// Deletes existing comments in the cloud to the application database.
        /// </summary>
        public Task SynchronizeForDeletingCommentsAsync();

        /// <summary>
        /// Updates existing comments in the cloud to the application database.
        /// </summary>
        public Task SynchronizeForUpdatingCommentsAsync();
    }
}

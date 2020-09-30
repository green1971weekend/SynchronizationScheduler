using Coravel.Invocable;
using SynchronizationScheduler.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace SynchronizationScheduler.Worker.ScheduleTasks
{
    /// <summary>
    /// Synchronization scheduler for posts models, invokes by the coravel service.
    /// </summary>
    public class PostSynchronizationScheduler : IInvocable
    {
        IPostSynchronizationService _postSynchronizationService;

        public PostSynchronizationScheduler(IPostSynchronizationService postSynchronizationService)
        {
            _postSynchronizationService = postSynchronizationService ?? throw new ArgumentNullException(nameof(postSynchronizationService));
        }

        /// <summary>
        /// Serves as a trigger for coravel.
        /// </summary>
        public async Task Invoke()
        {
            Console.WriteLine("Synchronizing post data...");
            await _postSynchronizationService.SynchronizeForAddingPostsAsync();
            await _postSynchronizationService.SynchronizeForDeletingPostsAsync();
            await _postSynchronizationService.SynchronizeForUpdatingPostsAsync();
        }
    }
}

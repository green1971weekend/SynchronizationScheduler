using Coravel.Invocable;
using SynchronizationScheduler.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace SynchronizationScheduler.Worker.ScheduleTasks
{
    /// <summary>
    /// Synchronization scheduler for person models, invokes by the coravel service.
    /// </summary>
    public class CommentSynchronizationScheduler : IInvocable
    {
        ICommentSynchronizationService _commentSynchronizationService;

        public CommentSynchronizationScheduler(ICommentSynchronizationService commentSynchronizationService)
        {
            _commentSynchronizationService = commentSynchronizationService ?? throw new ArgumentNullException(nameof(commentSynchronizationService));
        }

        /// <summary>
        /// Serves as a trigger for coravel.
        /// </summary>
        public async Task Invoke()
        {
            Console.WriteLine("Synchronizing comment data...");
            await _commentSynchronizationService.SynchronizeForAddingCommentsAsync();
            await _commentSynchronizationService.SynchronizeForDeletingCommentsAsync();
            await _commentSynchronizationService.SynchronizeForUpdatingCommentsAsync();
        }
    }
}

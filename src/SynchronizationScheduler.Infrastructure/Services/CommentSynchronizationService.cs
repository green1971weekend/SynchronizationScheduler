using Microsoft.EntityFrameworkCore;
using SynchronizationScheduler.Application.DTO;
using SynchronizationScheduler.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronizationScheduler.Infrastructure.Services
{
    ///<inheritdoc cref="ICommentSynchronizationService"/>
    public class CommentSynchronizationService : ICommentSynchronizationService
    {
        private readonly ICloudManager _cloudManager;

        private readonly ICommentManager _commentManager;

        private readonly IPostManager _postManager;

        /// <summary>
        /// Constructor for resolving Cloud manager, Post manager from DI container.
        /// </summary>
        /// <param name="cloudManager">Cloud Manager.</param>
        /// <param name="postManager">Post Manager.</param>
        /// /// <param name="commentManager">Comment Manager.</param>
        public CommentSynchronizationService(ICloudManager cloudManager, IPostManager postManager, ICommentManager commentManager)
        {
            _cloudManager = cloudManager ?? throw new ArgumentNullException(nameof(cloudManager));
            _postManager = postManager ?? throw new ArgumentNullException(nameof(postManager));
            _commentManager = commentManager ?? throw new ArgumentNullException(nameof(commentManager));
        }

        ///<inheritdoc/>
        public async Task SynchronizeForAddingCommentsAsync()
        {
            var cloudComments = await _cloudManager.GetComments().ToListAsync();
            var applicationComments = (await _commentManager.GetCommentsWithoutTrackingAsync()).ToList();

            var cloudCommentsIds = cloudComments.Select(c => c.Id).ToList();
            var applicationCommentsIds = applicationComments.Select(c => c.CloudId).ToList();

            var idsForSync = cloudCommentsIds.Except(applicationCommentsIds);

            var commentsForSync = cloudComments.Join(idsForSync,
                cloudComment => cloudComment.Id,
                newId => newId,
                (cloudComment, newId) => cloudComment);

            if (commentsForSync.Any())
            {
                foreach (var comment in commentsForSync)
                {
                    var applicationPosts = await _postManager.GetPostsWithoutTrackingAsync();
                    var postId = applicationPosts.FirstOrDefault(post => post.CloudId == comment.PostId).Id;

                    var commentDto = new CommentDto
                    {
                        CloudId = comment.Id,
                        PostId = postId,
                        Email = comment.Email,
                        Name = comment.Name
                    };

                    await _commentManager.CreateCommentAsync(commentDto);
                }
            }
        }

        ///<inheritdoc/>
        public async Task SynchronizeForDeletingCommentsAsync()
        {
            var cloudComments = await _cloudManager.GetComments().ToListAsync();
            var applicationComments = (await _commentManager.GetCommentsWithoutTrackingAsync()).ToList();

            var cloudCommentsIds = cloudComments.Select(c => c.Id).ToList();
            var applicationCommentsIds = applicationComments.Select(c => c.CloudId).ToList();

            var idsForSync = applicationCommentsIds.Except(cloudCommentsIds);

            if (idsForSync.Any())
            {
                foreach (var id in idsForSync)
                {
                    await _commentManager.DeleteCommentByCloudIdAsync(id);
                }
            }
        }

        ///<inheritdoc/>
        public async Task SynchronizeForUpdatingCommentsAsync()
        {
            var cloudComments = await _cloudManager.GetComments().ToListAsync();
            var applicationComments = (await _commentManager.GetCommentsWithoutTrackingAsync()).ToList();

            foreach (var appComment in applicationComments)
            {
                var isUpdated = false;

                var applicationPost = await _postManager.GetPostWithoutTrackingAsync(appComment.PostId);
                var cloudComment = cloudComments.FirstOrDefault(comment => comment.Id == appComment.CloudId);

                if (applicationPost.CloudId != cloudComment.PostId)
                {
                    var postId = (await _postManager.GetPostWithoutTrackingByCloudIdAsync(cloudComment.Id)).Id;

                    appComment.PostId = postId; // !!!
                    isUpdated = true;
                }

                if (appComment.Name != cloudComment.Name)
                {
                    appComment.Name = cloudComment.Name;
                    isUpdated = true;
                }

                if (appComment.Email != cloudComment.Email)
                {
                    appComment.Email = cloudComment.Email;
                    isUpdated = true;
                }

                if (appComment.Body != cloudComment.Body)
                {
                    appComment.Body = cloudComment.Body;
                    isUpdated = true;
                }

                if (isUpdated)
                {
                    await _commentManager.UpdateCommentAsync(appComment);
                }
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SynchronizationScheduler.Application.DTO;
using SynchronizationScheduler.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynchronizationScheduler.Infrastructure.Services
{
    ///<inheritdoc cref="IPostSynchronizationService"/>
    public class PostSynchronizationService : IPostSynchronizationService
    {
        private readonly ICloudManager _cloudManager;

        private readonly IPostManager _postManager;

        private readonly IPersonManager _personManager;

        /// <summary>
        /// Constructor for resolving Cloud manager, Post manager from DI container.
        /// </summary>
        /// <param name="cloudManager">Cloud Manager.</param>
        /// <param name="postManager">Post Manager.</param>
        /// /// <param name="personManager">Person Manager.</param>
        public PostSynchronizationService(ICloudManager cloudManager, IPostManager postManager, IPersonManager personManager)
        {
            _cloudManager = cloudManager ?? throw new ArgumentNullException(nameof(cloudManager));
            _postManager = postManager ?? throw new ArgumentNullException(nameof(postManager));
            _personManager = personManager ?? throw new ArgumentNullException(nameof(personManager));
        }

        ///<inheritdoc/>
        public async Task SynchronizeForAddingPostsAsync()
        {
            var cloudPosts = await _cloudManager.GetPosts().ToListAsync();
            var applicationPosts = (await _postManager.GetPostsWithoutTrackingAsync()).ToList();

            var cloudPostsIds = cloudPosts.Select(p => p.Id).ToList();
            var applicationPostsIds = applicationPosts.Select(p => p.CloudId).ToList();

            var idsForSync = cloudPostsIds.Except(applicationPostsIds);

            var postsForSync = cloudPosts.Join(idsForSync,
                cloudPost => cloudPost.Id,
                newId => newId,
                (cloudPost, newId) => cloudPost);

            if (postsForSync.Any())
            {
                foreach (var post in postsForSync)
                {
                    var applicationPersons = await _personManager.GetPeopleWithoutTrackingAsync();
                    var personId = applicationPersons.SingleOrDefault(person => person.CloudId == post.PersonId).Id;

                    var postDto = new PostDto
                    {
                        CloudId = post.Id,
                        PersonId = personId,
                        Title = post.Title,
                        Body = post.Body
                    };

                    await _postManager.CreatePostAsync(postDto);
                }
            }
        }

        ///<inheritdoc/>
        public async Task SynchronizeForDeletingPostsAsync()
        {
            var cloudPosts = await _cloudManager.GetPosts().ToListAsync();
            var applicationPosts = (await _postManager.GetPostsWithoutTrackingAsync()).ToList();

            var cloudPostsIds = cloudPosts.Select(p => p.Id).ToList();
            var applicationPostsIds = applicationPosts.Select(p => p.CloudId).ToList();

            var idsForSync = applicationPostsIds.Except(cloudPostsIds);

            if (idsForSync.Any())
            {
                foreach (var id in idsForSync)
                {
                    await _postManager.DeletePostByCloudIdAsync(id);
                }
            }
        }

        ///<inheritdoc/>
        public async Task SynchronizeForUpdatingPostsAsync()
        {
            var cloudPosts = await _cloudManager.GetPosts().ToListAsync();
            var applicationPosts = (await _postManager.GetPostsWithoutTrackingAsync()).ToList();

            foreach (var appPost in applicationPosts)
            {
                var isUpdated = false;

                var applicationPerson = await _personManager.GetPersonWithoutTrackingAsync(appPost.PersonId);
                var cloudPost = cloudPosts.FirstOrDefault(post => post.Id == appPost.CloudId);

                if (applicationPerson.CloudId != cloudPost.PersonId)
                {
                    var personId = (await _personManager.GetPersonWithoutTrackingByCloudIdAsync(cloudPost.PersonId)).Id;
                    appPost.PersonId = personId;
                    isUpdated = true;
                }

                if (appPost.Title != cloudPost.Title)
                {
                    appPost.Title = cloudPost.Title;
                    isUpdated = true;
                }

                if (appPost.Body != cloudPost.Body)
                {
                    appPost.Body = cloudPost.Body;
                    isUpdated = true;
                }

                if (isUpdated)
                {
                    await _postManager.UpdatePostAsync(appPost);
                }
            }
        }
    }
}

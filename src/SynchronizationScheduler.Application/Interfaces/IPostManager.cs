using SynchronizationScheduler.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynchronizationScheduler.Application.Interfaces
{
    /// <summary>
    /// Manager for interacting with the application post data. Works by the Repositry principle pattern but can include more functionality(for example some business logic).
    /// </summary>
    public interface IPostManager
    {
        /// <summary>
        /// Creates a new post and saving it to the database.
        /// </summary>
        /// <param name="postDto">Data transfer object.</param>
        public Task<int> CreatePostAsync(PostDto postDto);

        /// <summary>
        /// Returns an existing post from the database.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public Task<PostDto> GetPostAsync(int id);

        /// <summary>
        /// Returns an existing post from the database without tracking the objects in the EF cache..
        /// </summary>
        /// <param name="id">Identifier.</param>
        public Task<PostDto> GetPostWithoutTrackingAsync(int id);

        /// <summary>
        /// Returns an existing post from database by the cloud identifier without tracking the objects in the EF cache.
        /// </summary>
        /// <param name="cloudId">Cloud identifier.</param>
        public Task<PostDto> GetPostWithoutTrackingByCloudIdAsync(int cloudId);

        /// <summary>
        /// Returns a full list of existing posts from the database.
        /// </summary>
        public Task<IEnumerable<PostDto>> GetPostsAsync();

        /// <summary>
        /// Returns a full list of existing posts from the database without tracking the objects in the EF cache.
        /// </summary>
        Task<IEnumerable<PostDto>> GetPostsWithoutTrackingAsync();

        /// <summary>
        /// Updates an existing post and saving to the database.
        /// </summary>
        /// <param name="postDto">Data transfer object.</param>
        public Task<int> UpdatePostAsync(PostDto postDto);

        /// <summary>
        /// Deletes an existing post from the database.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public Task<int> DeletePostAsync(int id);

        /// <summary>
        /// Deletes an existing post from the database by the cloud id. Solves a problem with different id value between cloud and application.
        /// </summary>
        /// <param name="id">Identifier.</param>
        Task<int> DeletePostByCloudIdAsync(int cloudId);
    }
}

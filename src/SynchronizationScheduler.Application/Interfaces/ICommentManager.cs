using SynchronizationScheduler.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SynchronizationScheduler.Application.Interfaces
{
    /// <summary>
    /// Manager for interacting with the application comment data. Works by the Repositry principle pattern but can include more functionality(for example some business logic).
    /// </summary>
    public interface ICommentManager
    {
        /// <summary>
        /// Creates a new comment and saving it to the database.
        /// </summary>
        /// <param name="postDto">Data transfer object.</param>
        public Task<int> CreateCommentAsync(CommentDto commentDto);

        /// <summary>
        /// Returns an existing comment from the database.
        /// </summary>
        /// <param name="postDto">Identifier.</param>
        public Task<CommentDto> GetCommentAsync(int id);

        /// <summary>
        /// Returns a full list of existing comments from the database.
        /// </summary>
        public Task<IEnumerable<CommentDto>> GetCommentsAsync();

        /// <summary>
        /// Returns a full list of existing comments from the database without tracking the objects in the EF cache.
        /// </summary>
        Task<IEnumerable<CommentDto>> GetCommentsWithoutTrackingAsync();

        /// <summary>
        /// Updates an existing comment and saving to the database.
        /// </summary>
        /// <param name="postDto">Data transfer object.</param>
        public Task<int> UpdateCommentAsync(CommentDto postDto);

        /// <summary>
        /// Deletes an existing comment from the database.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public Task<int> DeleteCommentAsync(int id);

        /// <summary>
        /// Deletes an existing comment from the database by the cloud id. Solves a problem with different id value between cloud and application.
        /// </summary>
        /// <param name="id">Identifier.</param>
        Task<int> DeleteCommentByCloudIdAsync(int cloudId);
    }
}

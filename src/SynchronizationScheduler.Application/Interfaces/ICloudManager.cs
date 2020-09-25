using SynchronizationScheduler.Domain.Models.Cloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SynchronizationScheduler.Application.Interfaces
{
    /// <summary>
    /// Manager for interacting with the cloud data. Works by the Repositry principle pattern but can include more functionality(for example some business logic).
    /// </summary>
    public interface ICloudManager
    {
        /// <summary>
        /// Returns IQueryable instruction for getting the full list of users from a database.
        /// </summary>
        public IQueryable<User> GetUsers();

        /// <summary>
        /// Returns IQueryable instruction for getting the full list of posts from a database.
        /// </summary>
        public IQueryable<Post> GetPosts();

        /// <summary>
        /// Returns IQueryable instruction for getting the full list of comments from a database.
        /// </summary>
        public IQueryable<Comment> GetComments();
    }
}

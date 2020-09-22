using Microsoft.EntityFrameworkCore;
using SynchronizationScheduler.Domain.Models.Cloud;

namespace SynchronizationScheduler.Application.Interfaces
{
    /// <summary>
    /// Cloud database context.
    /// </summary>
    public interface ICloudDbContext
    {
        /// <summary>
        /// List of users contained in the cloud database.
        /// </summary>
        DbSet<User> Users { get; set; }

        /// <summary>
        /// List of posts contained in the cloud database.
        /// </summary>
        DbSet<Post> Posts { get; set; }

        /// <summary>
        /// List of comments contained in the cloud database.
        /// </summary>
        DbSet<Comment> Comments { get; set; }
    }
}

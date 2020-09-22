using Microsoft.EntityFrameworkCore;
using SynchronizationScheduler.Application.Interfaces;
using SynchronizationScheduler.Domain.Models.Cloud;

namespace SynchronizationScheduler.Infrastructure.CloudContext
{
    /// <inheritdoc cref="ICloudDbContext"/>
    public class CloudDbContext : DbContext, ICloudDbContext
    {
        /// <summary>
        /// Passing configuration connection options to the base constructor of the DbContext.
        /// </summary>
        /// <param name="options">Options of the DbContext such as a connection string to specific database.</param>
        public CloudDbContext(DbContextOptions<CloudDbContext> options)
            : base(options) { }


        /// <inheritdoc/>
        public DbSet<User> Users { get; set; }

        /// <inheritdoc/>
        public DbSet<Post> Posts { get; set; }

        /// <inheritdoc/>
        public DbSet<Comment> Comments { get; set; }

    }
}

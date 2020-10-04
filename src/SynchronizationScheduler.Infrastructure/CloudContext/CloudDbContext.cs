using Microsoft.EntityFrameworkCore;
using SynchronizationScheduler.Application.Interfaces;
using SynchronizationScheduler.Domain.Models.Cloud;

namespace SynchronizationScheduler.Infrastructure.CloudContext
{
    /// <inheritdoc cref="ICloudDbContext"/>
    public class CloudDbContext : DbContext, ICloudDbContext
    {
        /// <summary>
        /// Default constructor. In the test project allows make an instance with mock.
        /// </summary>
        public CloudDbContext() { }

        /// <summary>
        /// Passing configuration connection options to the base constructor of the DbContext.
        /// </summary>
        /// <param name="options">Options of the DbContext such as a connection string to specific database.</param>
        public CloudDbContext(DbContextOptions<CloudDbContext> options)
            : base(options) { }


        /// <inheritdoc/>
        public virtual DbSet<User> Users { get; set; }

        /// <inheritdoc/>
        public virtual DbSet<Post> Posts { get; set; }

        /// <inheritdoc/>
        public virtual DbSet<Comment> Comments { get; set; }

    }
}

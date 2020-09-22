using Microsoft.EntityFrameworkCore;
using SynchronizationScheduler.Domain.Models.Application;

namespace SynchronizationScheduler.Application.Interfaces
{
    /// <summary>
    /// Application database context.
    /// </summary>
    public interface IApplicationDbContext
    {
        /// <summary>
        /// List of persons contained in the application database.
        /// </summary>
        DbSet<Person> Persons { get; set; }

        /// <summary>
        /// List of posts contained in the application database.
        /// </summary>
        DbSet<Post> Posts { get; set; }

        /// <summary>
        /// List of comments contained in the application database.
        /// </summary>
        DbSet<Comment> Comments { get; set; }
    }
}

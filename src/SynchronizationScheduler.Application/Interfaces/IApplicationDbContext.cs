using Microsoft.EntityFrameworkCore;
using SynchronizationScheduler.Domain.Models.Application;
using System.Threading;
using System.Threading.Tasks;

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

        /// <summary>
        /// Asynchronously saves all current changes to the database. Need to be defined here for using it in the application managers.
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

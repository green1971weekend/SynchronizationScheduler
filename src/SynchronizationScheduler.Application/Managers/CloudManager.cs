using Microsoft.EntityFrameworkCore;
using SynchronizationScheduler.Application.Interfaces;
using SynchronizationScheduler.Domain.Models.Cloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronizationScheduler.Application.Managers
{

    /// <inheritdoc cref="ICloudManager"/>
    public class CloudManager : ICloudManager
    {
        private readonly ICloudDbContext _context;

        /// <summary>
        /// Constructor for resolving CloudDbContext from the DI container.
        /// </summary>
        /// <param name="context">Cloud context placed into DI container.</param>
        public CloudManager(ICloudDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public IQueryable<User> GetUsers()
        {
            return _context.Users;
        }


        /// <inheritdoc/>
        public IQueryable<Post> GetPosts()
        {
            return _context.Posts;
        }

        /// <inheritdoc/>
        public IQueryable<Comment> GetComments()
        {
            return _context.Comments;
        }
    }
}

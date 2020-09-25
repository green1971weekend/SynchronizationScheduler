using SynchronizationScheduler.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SynchronizationScheduler.Application.Managers
{
    /// <inheritdoc cref="IPostManager"/>
    public class PostManager : IPostManager
    {
        private readonly IApplicationDbContext _context;

        public PostManager(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}

using SynchronizationScheduler.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SynchronizationScheduler.Application.Managers
{
    /// <inheritdoc cref="ICommentManager"/>
    class CommentManager : ICommentManager
    {
        private readonly IApplicationDbContext _context;

        public CommentManager(IApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}

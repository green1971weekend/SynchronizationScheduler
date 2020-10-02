using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SynchronizationScheduler.Application.DTO;
using SynchronizationScheduler.Application.Interfaces;
using SynchronizationScheduler.Domain.Models.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SynchronizationScheduler.Application.Managers
{
    /// <inheritdoc cref="ICommentManager"/>
    public class CommentManager : ICommentManager
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Manager constructor which resolves services below.
        /// </summary>
        /// <param name="context">Application database context.</param>
        /// <param name="mapper">Automapper.</param>
        public CommentManager(IApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public async Task<int> CreateCommentAsync(CommentDto commentDto)
        {
            var comment = _mapper.Map<CommentDto, Comment>(commentDto);
            await _context.Comments.AddAsync(comment);

            return await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<CommentDto> GetCommentAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(comment => comment.Id == id);
            return _mapper.Map<Comment, CommentDto>(comment);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CommentDto>> GetCommentsAsync()
        {
            var comments = await _context.Comments.ToListAsync();
            return _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDto>>(comments);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CommentDto>> GetCommentsWithoutTrackingAsync()
        {
            var comments = await _context.Comments.AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDto>>(comments);
        }

        /// <inheritdoc/>
        public async Task<int> UpdateCommentAsync(CommentDto commentDto)
        {
            var comment = _mapper.Map<CommentDto, Comment>(commentDto);
            _context.Comments.Update(comment);

            return await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<int> DeleteCommentAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(comment => comment.Id == id);
            _context.Comments.Remove(comment);

            return await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<int> DeleteCommentByCloudIdAsync(int cloudId)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(comment => comment.CloudId == cloudId);
            _context.Comments.Remove(comment);

            return await _context.SaveChangesAsync();
        }
    }
}

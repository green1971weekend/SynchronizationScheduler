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
    /// <inheritdoc cref="IPostManager"/>
    public class PostManager : IPostManager
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Manager constructor which resolves services below.
        /// </summary>
        /// <param name="context">Application database context.</param>
        /// <param name="mapper">Automapper.</param>
        public PostManager(IApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public async Task<int> CreatePostAsync(PostDto postDto)
        {
            var post = _mapper.Map<PostDto, Post>(postDto);
            await _context.Posts.AddAsync(post);

            return await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<PostDto> GetPostAsync(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(post => post.Id == id);
            return _mapper.Map<Post, PostDto>(post);
        }

        /// <inheritdoc/>
        public async Task<PostDto> GetPostWithoutTrackingAsync(int id)
        {
            var post = await _context.Posts.AsNoTracking().FirstOrDefaultAsync(post => post.Id == id);
            return _mapper.Map<Post, PostDto>(post);
        }

        /// <inheritdoc/>
        public async Task<PostDto> GetPostWithoutTrackingByCloudIdAsync(int cloudId)
        {
            var post = await _context.Posts.AsNoTracking().FirstOrDefaultAsync(post => post.CloudId == cloudId);
            return _mapper.Map<Post, PostDto>(post);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<PostDto>> GetPostsAsync()
        {
            var posts = await _context.Posts.ToListAsync();
            return _mapper.Map<IEnumerable<Post>, IEnumerable<PostDto>>(posts);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<PostDto>> GetPostsWithoutTrackingAsync()
        {
            var posts = await _context.Posts.AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<Post>, IEnumerable<PostDto>>(posts);
        }

        /// <inheritdoc/>
        public async Task<int> UpdatePostAsync(PostDto postDto)
        {
            var post = _mapper.Map<PostDto, Post>(postDto);
            _context.Posts.Update(post);

            return await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<int> DeletePostAsync(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(post => post.Id == id);
            _context.Posts.Remove(post);

            return await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<int> DeletePostByCloudIdAsync(int cloudId)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(post => post.CloudId == cloudId);
            _context.Posts.Remove(post);

            return await _context.SaveChangesAsync();
        }
    }
}

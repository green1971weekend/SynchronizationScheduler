using Microsoft.EntityFrameworkCore;
using SynchronizationScheduler.Application.Interfaces;
using SynchronizationScheduler.Domain.Models.Application;
using SynchronizationScheduler.Infrastructure.ApplicationContext.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SynchronizationScheduler.Infrastructure.ApplicationContext
{
    /// <inheritdoc cref="IApplicationDbContext"/>
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        /// <summary>
        /// Passing configuration connection options to the base constructor of the DbContext.
        /// </summary>
        /// <param name="options">Options of the DbContext such as a connection string to specific database.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options) { }

        /// <inheritdoc/>
        public DbSet<Person> Persons { get; set; }

        /// <inheritdoc/>
        public DbSet<Post> Posts { get; set; }

        /// <inheritdoc/>
        public DbSet<Comment> Comments { get; set; }

        /// <summary>
        /// Applying additional models configuration thanks to ApplyConfiguration method.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}

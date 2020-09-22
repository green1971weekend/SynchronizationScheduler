using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SynchronizationScheduler.Domain.Models.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace SynchronizationScheduler.Infrastructure.ApplicationContext.Configurations
{
    /// <summary>
    /// Entity framework configuration for comment model. Creation of additional configuration implemented thanks to IEntityTypeConfiguration.
    /// </summary>
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        /// <summary>
        /// Creates additional configuration for a model in the separate class instead of OnModelCreating method located in ApplicationDbContext.
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments")
                .HasKey(c => c.Id);
        }
    }
}

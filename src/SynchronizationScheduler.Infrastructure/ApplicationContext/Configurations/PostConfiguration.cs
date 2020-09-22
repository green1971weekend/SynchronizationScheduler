using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SynchronizationScheduler.Domain.Models.Application;

namespace SynchronizationScheduler.Infrastructure.ApplicationContext.Configurations
{
    /// <summary>
    /// Entity framework configuration for post model. Creation of additional configuration implemented thanks to IEntityTypeConfiguration.
    /// </summary>
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        /// <summary>
        /// Creates additional configuration for a model in the separate class instead of OnModelCreating method located in ApplicationDbContext.
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts")
                .HasKey(p => p.Id);
        }
    }
}

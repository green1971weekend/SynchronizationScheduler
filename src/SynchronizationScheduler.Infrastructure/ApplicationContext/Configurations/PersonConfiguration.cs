using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SynchronizationScheduler.Domain.Models.Application;

namespace SynchronizationScheduler.Infrastructure.ApplicationContext.Configurations
{
    /// <summary>
    /// Entity framework configuration for person model. Creation of additional configuration implemented thanks to IEntityTypeConfiguration.
    /// </summary>
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        /// <summary>
        /// Creates additional configuration for a model in the separate class instead of OnModelCreating method located in ApplicationDbContext.
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons")
                .HasKey(p => p.Id);
        }
    }
}

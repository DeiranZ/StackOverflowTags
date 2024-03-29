using Microsoft.EntityFrameworkCore;

namespace StackOverflowTags.Infrastructure.Persistence
{
    public class StackOverflowTagsDbContext : DbContext
    {
        public StackOverflowTagsDbContext(DbContextOptions<StackOverflowTagsDbContext> options) : base(options) { }

        public DbSet<Domain.Entities.Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Domain.Entities.Tag>();
        }

    }
}

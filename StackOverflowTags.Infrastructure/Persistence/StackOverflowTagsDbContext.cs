using Microsoft.EntityFrameworkCore;

namespace StackOverflowTags.Infrastructure.Persistence
{
    public class StackOverflowTagsDbContext : DbContext
    {
        public StackOverflowTagsDbContext(DbContextOptions<StackOverflowTagsDbContext> options) : base(options) { }

        public DbSet<Domain.Entities.Tag> Tags { get; set; }

    }
}

using Microsoft.EntityFrameworkCore;
using StackOverflowTags.Domain.Entities;
using StackOverflowTags.Domain.Interfaces;
using StackOverflowTags.Infrastructure.Persistence;

namespace StackOverflowTags.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly StackOverflowTagsDbContext dbContext;

        public TagRepository(StackOverflowTagsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Tag>> GetAll()
        {
            return await dbContext.Tags.ToListAsync();
        }
    }
}

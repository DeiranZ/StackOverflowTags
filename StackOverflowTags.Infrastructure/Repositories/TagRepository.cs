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

        public void Clear()
        {
            dbContext.Tags.ExecuteDelete();
        }

        public void Create(Tag tag)
        {
            dbContext.Add(tag);
            dbContext.SaveChanges();
        }

        public void Create(IEnumerable<Tag> tags)
        {
            dbContext.AddRange(tags);
            dbContext.SaveChanges();
        }

        public IEnumerable<Tag> GetAll()
        {
            return dbContext.Tags.ToList();
        }
    }
}

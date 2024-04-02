using Microsoft.EntityFrameworkCore;
using StackOverflowTags.Domain.Entities;
using StackOverflowTags.Domain.Interfaces;
using StackOverflowTags.Domain.Models;
using StackOverflowTags.Infrastructure.Persistence;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace StackOverflowTags.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly StackOverflowTagsDbContext dbContext;

        public TagRepository(StackOverflowTagsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Clear()
        {
            await dbContext.Tags.ExecuteDeleteAsync();
        }

        public async Task Create(Tag tag)
        {
            dbContext.Add(tag);
            await dbContext.SaveChangesAsync();
        }

        public async Task Create(IEnumerable<Tag> tags)
        {
            dbContext.AddRange(tags);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tag>> GetAll()
        {
            return await dbContext.Tags.ToListAsync();
        }

        public async Task<IEnumerable<Tag>> Get(TagParameters tagParameters)
        {
            var tags = dbContext.Tags.AsNoTracking();

            ApplySort(ref tags, tagParameters.OrderBy);

            return await tags
                .Skip((tagParameters.PageNumber - 1) * tagParameters.PageSize)
                .Take(tagParameters.PageSize)
                .ToListAsync();
        }

        private void ApplySort(ref IQueryable<Tag> tags, string orderByQueryString)
        {
            if (!tags.Any())
                return;

            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                tags = tags.OrderBy(x => x.Count).Reverse();
                return;
            }

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Tag).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                tags = tags.OrderBy(x => x.Count).Reverse();
                return;
            }

            tags = tags.OrderBy(orderQuery);
        }
    }
}

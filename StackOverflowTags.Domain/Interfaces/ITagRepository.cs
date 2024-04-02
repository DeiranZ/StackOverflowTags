using StackOverflowTags.Domain.Models;

namespace StackOverflowTags.Domain.Interfaces
{
    public interface ITagRepository
    {
        Task Create(Entities.Tag tag);
        Task Create(IEnumerable<Entities.Tag> tags);
        Task<IEnumerable<Entities.Tag>> GetAll();
        Task<PagedList<Entities.Tag>> Get(TagParameters tagParameters);
        Task Clear();
    }
}

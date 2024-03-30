namespace StackOverflowTags.Domain.Interfaces
{
    public interface ITagRepository
    {
        Task Create(Entities.Tag tag);
        Task Create(IEnumerable<Entities.Tag> tags);
        Task<IEnumerable<Entities.Tag>> GetAll();
    }
}

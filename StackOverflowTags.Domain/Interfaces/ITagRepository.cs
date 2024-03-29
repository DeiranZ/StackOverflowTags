namespace StackOverflowTags.Domain.Interfaces
{
    public interface ITagRepository
    {
        Task<IEnumerable<Entities.Tag>> GetAll();
    }
}

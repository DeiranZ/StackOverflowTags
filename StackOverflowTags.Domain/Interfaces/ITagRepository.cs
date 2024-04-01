namespace StackOverflowTags.Domain.Interfaces
{
    public interface ITagRepository
    {
        void Create(Entities.Tag tag);
        void Create(IEnumerable<Entities.Tag> tags);
        IEnumerable<Entities.Tag> GetAll();
        void Clear();
    }
}

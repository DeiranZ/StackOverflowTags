namespace StackOverflowTags.Domain.Interfaces
{
    public interface ITagTableInitializer
    {
        Task Initialize();
        Task Reinitialize();
    }
}

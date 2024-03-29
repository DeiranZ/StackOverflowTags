namespace StackOverflowTags.Domain.Entities
{
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
        public int Count {  get; set; }
        public double Percentage { get; set; }

    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflowTags.Domain.Entities
{
    [Table("Tags")]
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
        public int Count {  get; set; }
        public float Percentage { get; set; }
    }
}

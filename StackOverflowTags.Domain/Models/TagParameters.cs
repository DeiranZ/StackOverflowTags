namespace StackOverflowTags.Domain.Models
{
    public class TagParameters
    {
        public TagParameters()
        {
            OrderBy = "percentage desc";
            _pageSize = 10;
        }


        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }

        public string OrderBy { get; set; }
    }
}

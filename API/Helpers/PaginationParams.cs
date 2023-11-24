namespace API.Helpers
{
    public class PaginationParams
    {
        private const int MaxPageSize = 50;
        public int pageNumber { get; set; } = 1;
        public int _pageSize = 20;

        public int PageSize { 
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

    }
}

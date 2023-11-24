using Microsoft.EntityFrameworkCore;

namespace API.Helpers
{
    public class PagedList<T> : List<T> 
    {
        public int TotalCount { get; set; }
        public int PageNumber { get;set; }
        public int ItemsPerPage { get; set; }
        public int TotalPages { get; set; }
        public double Income { get; set; }  
        public double Expense { get; set; }

        public PagedList(IEnumerable<T> items, int count, int itemsPerPage, int pageNumber)
        {
            PageNumber = pageNumber;
            ItemsPerPage = itemsPerPage;
            TotalCount = count;
            TotalPages = (int) Math.Ceiling(TotalCount/((double) itemsPerPage));
            AddRange(items);
        }

        public static  PagedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count =  source.Count();
            var items =  source.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageSize, pageNumber);
        }
    }
}

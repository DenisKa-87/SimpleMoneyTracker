namespace API.Helpers
{
    public class PaginationHeader
    {
        public PaginationHeader(int itemsPerPage, int currentPage, int totalPages, int totalItems, double income, double expense)
        {
            ItemsPerPage = itemsPerPage;
            CurrentPage = currentPage;
            TotalPages = totalPages;
            TotalItems = totalItems;
            Income = income;
            Expense = expense;
        }

        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set;}
        public int TotalItems { get;set; }
        public double Income { get; set; }  
        public double Expense { get; set; }
    }
}

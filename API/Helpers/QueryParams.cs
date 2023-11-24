namespace API.Helpers
{
    public class QueryParams : PaginationParams
    {
        public DateTime MinDate;
        public DateTime MaxDate;
        public int RecordType = 1;
        public int CategoryId = int.MinValue;
        public string Order;

    }
}
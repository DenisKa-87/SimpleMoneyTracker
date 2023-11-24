using API.Entities;
using API.Helpers;
using System.Text.Json;

namespace API.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, PagedList<Record> records )
        {
            var paginationHeader = new PaginationHeader( records.ItemsPerPage, records.PageNumber, records.TotalPages, records.TotalCount,
                records.Income, records.Expense);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, options));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}

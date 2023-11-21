using API.DTO;
using API.Enitities;

namespace API.Interfaces
{
    public interface IRecordsRepository
    {
        Record Add(RecordDto record, AppUser userId);
        void UpdateRecord(Record record, RecordDto recordDto);
        void DeleteRecord(int id);
        //Task<PagedList<Record>> GetRecordsAsync(QueryParams queryParams, int userId);
       // Task<double> GetTotalRecordsAsync(QueryParams queryParams, int userId);
    }
}

using API.DTO;
using API.Enitities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{

    public interface IRecordsController
    {
        
        Task<ActionResult<ResponseRecordDto>> AddRecord(RecordDto recordDto);
        Task<ActionResult<Record>> UpdateRecord(RecordDto recordDto, int id);
        Task<ActionResult<IEnumerable<ResponseRecordDto>>> GetRecords([FromQuery] string CategoryId,
                                                                      [FromQuery] string RecordType,
                                                                      [FromQuery] string MinDate,
                                                                      [FromQuery] string MaxDate,
                                                                      [FromQuery] string itemsPerPage,
                                                                      [FromQuery] string pageNumber,
                                                                      [FromQuery] string Order);
        Task<ActionResult> DeleteRecord(int id);
        Task<double> GetSummary([FromQuery] string CategoryId,
                                             [FromQuery] string RecordType,
                                             [FromQuery] string MinDate,
                                             [FromQuery] string MaxDate
                                             );
    }
}

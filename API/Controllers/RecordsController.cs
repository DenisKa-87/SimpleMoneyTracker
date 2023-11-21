using API.DTO;
using API.Enitities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RecordsController : BaseApiController, IRecordsController
    {
        public Task<ActionResult<ResponseRecordDto>> AddRecord(RecordDto recordDto)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> DeleteRecord(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<ResponseRecordDto>>> GetRecords([FromQuery] string CategoryId,
            [FromQuery] string RecordType,
            [FromQuery] string MinDate,
            [FromQuery] string MaxDate,
            [FromQuery] string itemsPerPage,
            [FromQuery] string pageNumber,
            [FromQuery] string Order)
        {
            throw new NotImplementedException();
        }

        public Task<double> GetSummary([FromQuery] string CategoryId, 
            [FromQuery] string RecordType, 
            [FromQuery] string MinDate, 
            [FromQuery] string MaxDate)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Record>> UpdateRecord(RecordDto recordDto, int id)
        {
            throw new NotImplementedException();
        }
    }
}

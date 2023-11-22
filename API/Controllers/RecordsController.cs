using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class RecordsController : BaseApiController, IRecordsController
    {
        private readonly IUnitOfWork _unitOfWork;

        public RecordsController(IUnitOfWork unitofWork)
        {
            _unitOfWork = unitofWork;
        }

        [HttpPost("add")]
        public async Task<ActionResult<ResponseRecordDto>> AddRecord(RecordDto recordDto)
        {
            var userName = User.Identity.Name;
            var user = await _unitOfWork.AccountRepository.GetUSerByEmailAsync(userName);
            if (user == null)
            {
                return BadRequest("Somethig went wrong: user has not been found");
            }
            var record = _unitOfWork.RecordsRepository.Add(recordDto, user);

            if (await _unitOfWork.Complete())
            {
                return Ok(CreateResponseRecordDto(record));
            }
            return BadRequest("Failed to add this record");
        }

        private ResponseRecordDto CreateResponseRecordDto(Record record)
        {
            return new ResponseRecordDto
            {
                Id = record.Id,
                Name = record.Name,
                Description = record.Description,
                Price = record.Value,
                Category = record.Category.Name,
                CategoryId = record.Category.Id,
                RecordType = record.RecordType,
                Date = record.Date
            };
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRecord(int id)
        {
            var userName = User.Identity.Name;
            var user = await _unitOfWork.AccountRepository.GetUserFullDataByEmailAsync(userName);
            var record =  user.Records.FirstOrDefault(x => x.Id == id);
            if (record == null)
            {
                return BadRequest("Could not delete this record");
            }
            _unitOfWork.RecordsRepository.DeleteRecord(record);
            if (await _unitOfWork.Complete())
                return Ok("Successfully deleted");
            return BadRequest("Could not delete this record");
        }

        [HttpGet]
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

        [HttpGet]
        public Task<double> GetSummary([FromQuery] string CategoryId, 
            [FromQuery] string RecordType, 
            [FromQuery] string MinDate, 
            [FromQuery] string MaxDate)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task<ActionResult<Record>> UpdateRecord(RecordDto recordDto, int id)
        {
            throw new NotImplementedException();
        }
    }
}

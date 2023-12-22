using API.Data;
using API.DTO;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            var record = user.Records.FirstOrDefault(x => x.Id == id);
            if (record == null)
            {
                return BadRequest("Could not delete this record");
            }
            _unitOfWork.RecordsRepository.DeleteRecord(record);
            if (await _unitOfWork.Complete())
                return Ok(new ResponseMessage("Successfully deleted"));
                //return Ok();
            return BadRequest("Could not delete this record");
        }

        [HttpGet("categories")]

        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            //var userId = User.Identity.GetUserId();
            //var categories = await _unitOfWork.AccountRepository.GetCategories(userId);
            var userName = User.Identity.Name;
            var user = await _unitOfWork.AccountRepository.GetUSerByEmailAsync(userName);
            var categories = user.Categories;
            var categoriesDto = new List<CategoryDto>();
            foreach (var category in categories)
            {
                categoriesDto.Add(new CategoryDto(category));
            }
            return Ok(categoriesDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseRecordDto>>> GetRecords(
                                           [FromQuery] string CategoryId,
                                           [FromQuery] string RecordType,
                                           [FromQuery] string MinDate,
                                           [FromQuery] string MaxDate,
                                           [FromQuery] string itemsPerPage,
                                           [FromQuery] string pageNumber,
                                           [FromQuery] string Order)
        {

            var userId = Int32.Parse(User.Identity.GetUserId());
            QueryParams queryParams = CreateQueryParams(CategoryId, RecordType, MinDate, MaxDate, itemsPerPage, pageNumber, Order);
            var records = await _unitOfWork.RecordsRepository.GetRecordsAsync(queryParams, userId);
            var response = new List<ResponseRecordDto>();

            foreach (var record in records)
            {
                response.Add(CreateResponseRecordDto(record));
            }
            Response.AddPaginationHeader(records);

            return Ok(response);
        }

        private QueryParams CreateQueryParams(string CategoryId, string RecordType, string MinDate, string MaxDate,
             string itemsPerPage, string PageNumber, string Order)
        {
            return new QueryParams()
            {
                CategoryId = CategoryId == null ? int.MinValue : Int32.Parse(CategoryId),
                MaxDate = (MaxDate == null) ? DateTime.MaxValue : DateTime.Parse(MaxDate),
                MinDate = (MinDate == null) ? DateTime.MinValue : DateTime.Parse(MinDate),
                RecordType = RecordType == null ? -1 : Int32.Parse(RecordType),
                pageNumber = PageNumber == null ? 1 : Int32.Parse(PageNumber),
                PageSize = itemsPerPage == null ? 20 : Int32.Parse(itemsPerPage),
                Order = Order
            };
        }

        [HttpGet("summary")]
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

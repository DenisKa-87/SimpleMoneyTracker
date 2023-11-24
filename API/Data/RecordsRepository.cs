using API.DTO;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class RecordsRepository :IRecordsRepository
    {
        private DataContext _context;

        public RecordsRepository(DataContext context)
        {
            _context = context;
        }

        public Record Add(RecordDto recordDto, AppUser user)
        {
            var category = GetCategoryByName(user, recordDto.Category);
            //user.Categories.Add(category);

            var record = CreateRecordFromRecordDto(recordDto, user, category);
            _context.Add(record);
            return record;
        }

        private Record CreateRecordFromRecordDto(RecordDto recordDto, AppUser user, Category category)
        {
            if (recordDto.Date > DateTime.UtcNow)
                recordDto.Date = DateTime.UtcNow;
            return new Record
            {
                AppUser = user,
                Name = recordDto.Name,
                Description = recordDto.Description,
                Value = recordDto.Price,
                RecordType = recordDto.RecordType,
                Date = recordDto.Date,
                Category = category
            };
        }

        private Category GetCategoryByName(AppUser user, string categoryName = null)
        {
            var category = user.Categories.Where(x => x.Name.ToLower() == categoryName.ToLower()).SingleOrDefault();
            if (category == null)
            {
                category = CreateCategory(user, categoryName);
                user.Categories.Add(category);
            }
                

            return category;
        }

        private Category CreateCategory(AppUser user, string categoryName)
        {
            return new Category
            {
                Name = categoryName == null || categoryName == "" ? "Uncategorized" : categoryName,
                User = user
            };
        }

        public void DeleteRecord(Record record)
        {
            _context.Attach(record);
            _context.Remove(record);
        }

        public void UpdateRecord(Record record, RecordDto recordDto)
        {
            record.Name = recordDto.Name;
            record.Description = recordDto.Description;
            record.Date = recordDto.Date;
            record.Value = recordDto.Price;
            record.RecordType = recordDto.RecordType;
            record.Category = GetCategoryByName(record.AppUser, recordDto.Category);
            record.CreatedAt = DateTime.UtcNow;
            _context.Entry(record).State = EntityState.Modified;
        }

        private IQueryable<Record> CreateQuery(IQueryable<Record> query, QueryParams queryParams)
        {
            if (queryParams.RecordType > 0)
                query = query.Where(x => x.RecordType == queryParams.RecordType);
            query = query.Where(x => queryParams.CategoryId < 0 ?
                                                                x.Category.Id >= 0 : x.Category.Id == queryParams.CategoryId);
            query = query.Where(x => x.Date.Date <= queryParams.MaxDate && x.Date.Date >= queryParams.MinDate);
            query = queryParams.Order switch
            {
                "date" => query.OrderBy(x => x.Date),
                "value" => query.OrderByDescending(x => x.Value),
                "category" => query.OrderBy(x => x.Category.Name),
                _ => query.OrderByDescending(x => x.Date)
            };
            return query;
        }

        public async Task<PagedList<Record>> GetRecordsAsync(QueryParams queryParams, int userId)
        {
            double expense = 0;
            double income = 0;
            var user = await _context.Users.Include(x => x.Records).Include(x => x.Categories)
                .SingleOrDefaultAsync(u => u.Id == userId);
            var query = user.Records.AsQueryable();
            query = CreateQuery(query, queryParams);
            var result = PagedList<Record>.Create(query, queryParams.pageNumber, queryParams.PageSize);
            foreach (var record in query)
            {
                switch (record.RecordType)
                {
                    case 1:
                        expense += record.Value;
                        break;
                    case 2:
                        income += record.Value;
                        break;
                }
            }
            result.Income = income;
            result.Expense = expense;
            return result;
        }

        public async Task<double> GetTotalRecordsAsync(QueryParams queryParams, int userId)
        {
            var user = await _context.Users.Include(x => x.Records).Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == userId);
            var query = user.Records.AsQueryable();
            query = CreateQuery(query, queryParams);
            return query.Sum(x => x.Value);
        }
    }
}

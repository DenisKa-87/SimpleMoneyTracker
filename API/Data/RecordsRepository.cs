using API.DTO;
using API.Entities;
using API.Interfaces;

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
            throw new NotImplementedException();
        }
    }
}

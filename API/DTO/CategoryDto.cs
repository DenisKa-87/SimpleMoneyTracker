using API.Entities;

namespace API.DTO
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public CategoryDto(Category category)
        {
            CategoryId = category.Id;
            Name = category.Name;
        }

    }
}

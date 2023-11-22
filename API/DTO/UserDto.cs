using API.Entities;

namespace API.DTO
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }   
        public string Token { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}

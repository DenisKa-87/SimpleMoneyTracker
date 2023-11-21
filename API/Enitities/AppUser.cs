using API.DTO;
using Microsoft.AspNetCore.Identity;

namespace API.Enitities
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public ICollection<Record> Records { get; set; }
        public ICollection<Category> Categories { get; set; }

        public AppUser(SignInDto signInDto)
        {
            Records = new List<Record>();
            Categories = new List<Category>();
        }
    }
}

using API.DTO;
using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Interfaces
{
    public interface IAccountRepository
    {
        Task<AppUser> GetUserFullDataByEmailAsync(string email);
        Task<AppUser> GetUSerByEmailAsync(string email);
        Task<IEnumerable<Category>> GetCategories(int userId);
        Task<IdentityResult> CreateAsync(AppUser newUser, string password);
        Task<string> CreateToken(AppUser user);
        Task<SignInResult> Login(AppUser user, string password);
        Task<IdentityResult> Delete(AppUser user);
    }
}

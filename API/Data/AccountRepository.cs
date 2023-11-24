using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AccountRepository :IAccountRepository
    {
        private DataContext _context;
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private ITokenService _tokenService;

        public AccountRepository(DataContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<IdentityResult> CreateAsync(AppUser newUser, string password)
        {
            return await _userManager.CreateAsync(newUser, password);
        }

        public async Task<string> CreateToken(AppUser user)
        {
            return await _tokenService.CreateToken(user);
        }

        public Task<IdentityResult> Delete(AppUser user)
        {
           return _userManager.DeleteAsync(user);
        }

        public async Task<IEnumerable<Category>> GetCategories(string  email)
        {
            var user = await GetUSerByEmailAsync(email);
            return user.Categories.ToArray();
        }

        public async Task<AppUser> GetUSerByEmailAsync(string email)
        {
            if(email==null)
                return null;
            return await _context.Users
                .Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.NormalizedUserName == email.ToUpper());
        }

        //public Task<AppUser> GetUserByIdWithCategoriesAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<AppUser> GetUserFullDataByEmailAsync(string email)
        {
            return await _context.Users.Include(x => x.Categories).Include(x => x.Records).FirstOrDefaultAsync(x => x.NormalizedUserName == email.ToUpper());
        }

        public async Task<SignInResult> Login(AppUser user, string password)
        {
           return await _signInManager.CheckPasswordSignInAsync(user, password, false);
        }
    }
}

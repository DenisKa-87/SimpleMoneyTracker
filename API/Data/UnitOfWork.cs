using API.Enitities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public IRecordsRepository RecordRepository => throw new NotImplementedException();

        public IAccountRepository UserRepository => throw new NotImplementedException();


        public UnitOfWork(DataContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, 
            ITokenService tokenService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}

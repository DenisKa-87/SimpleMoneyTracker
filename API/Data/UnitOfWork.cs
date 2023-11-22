using API.Entities;
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

        public IRecordsRepository RecordsRepository => new RecordsRepository(_context);

        public IAccountRepository AccountRepository => new AccountRepository(_context, _userManager, _signInManager, _tokenService);


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
            var complete = await _context.SaveChangesAsync() > 0;
            return complete;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}

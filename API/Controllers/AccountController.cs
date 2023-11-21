using API.DTO;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class AccountController : BaseApiController, IAccountController
    {
        [HttpDelete]
        public Task<ActionResult<string>> Delete()
        {
            throw new NotImplementedException();
        }
        [HttpPost("login")]
        public Task<ActionResult<UserDto>> Login(LogInDto loginDto)
        {
            throw new NotImplementedException();
        }

        [HttpPost("signin")]
        public Task<ActionResult<UserDto>> SignIn(SignInDto registerDto)
        {
            throw new NotImplementedException();
        }        
    }
}

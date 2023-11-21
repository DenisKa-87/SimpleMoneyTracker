using API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IAccountController
    {
        
        Task<ActionResult<UserDto>> SignIn(SignInDto registerDto);

        Task<ActionResult<UserDto>> Login(LogInDto loginDto);

        Task<ActionResult<string>> Delete();
    }

    
}

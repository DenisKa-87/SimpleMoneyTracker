using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace API.Controllers
{
    
    public class AccountController : BaseApiController, IAccountController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<string>> Delete()
        {
            var userName = User.Identity.Name;
            var user = await _unitOfWork.AccountRepository.GetUSerByEmailAsync(userName);
            if (user == null)
            {
                return BadRequest();
            }
            var result = await  _unitOfWork.AccountRepository.Delete(user);
            if (!result.Succeeded)
            {
                return BadRequest("User was not deleted!");
            }
            return Ok("User was deleted successfully!");
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LogInDto loginDto)
        {
            var user = await _unitOfWork.AccountRepository.GetUSerByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return BadRequest("Password or email is incorrect");
            }
            var result = await _unitOfWork.AccountRepository.Login(user, loginDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest("Password or email is incorrect");
            }
            return Ok(new UserDto
            {
                Email = loginDto.Email,
                Name = user.Name,
                Token = await _unitOfWork.AccountRepository.CreateToken(user),
                Categories = user.Categories
            });
        }

        [HttpPost("signin")]
        public async Task<ActionResult<UserDto>> SignIn(SignInDto registerDto)
        {
            AppUser newUser = CreateUser(registerDto);
            var result = await _unitOfWork.AccountRepository.CreateAsync(newUser, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok(new UserDto
            {
                Name = newUser.Name,
                Email = newUser.Email,
                Token = await _unitOfWork.AccountRepository.CreateToken(newUser)
            });

        }

        private AppUser CreateUser(SignInDto registerDto)
        {
            return new AppUser
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                Records = new List<Record>(),
                Categories = new List<Category>()
            };
        }
    }
}

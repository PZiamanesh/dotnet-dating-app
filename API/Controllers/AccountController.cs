using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly DatingDbContextEF _context;
        private readonly ITokenService _tokenService;

        public AccountController(
            DatingDbContextEF context,
            ITokenService tokenService
            )
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.UserName))
            {
                return BadRequest("user is already taken");
            }

            return Ok();

            //using var hashAlgorithm = new HMACSHA512();

            //var newUser = new AppUser
            //{
            //    UserName = registerDto.UserName.ToLower(),
            //    PasswordHash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            //    PasswordSalt = hashAlgorithm.Key
            //};

            //_context.AppUsers.Add(newUser);
            //await _context.SaveChangesAsync();

            //var token = _tokenService.GetToken(newUser);
            //var userDto = new UserDto() { Token = token, Username = newUser.UserName };

            //return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            var user = await _context.AppUsers
                .Include(u => u.Photos)
                .FirstOrDefaultAsync(x => x.UserName == login.Username.ToLower());

            if (user == null)
            {
                return Unauthorized("incorrect username");
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var currentHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

            if (currentHash.Length != user.PasswordHash.Length)
            {
                return Unauthorized("incorrect password");
            }

            for (int i = 0; i < currentHash.Length; i++)
            {
                if (currentHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("incorrect password");
                }
            }

            var userDto = new UserDto()
            {
                Token = _tokenService.GetToken(user),
                Username = login.Username,
                PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain)?.Url
            };

            return Ok(userDto);
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.AppUsers.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }
    }
}

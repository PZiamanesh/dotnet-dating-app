using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
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
        private readonly IMapper mapper;

        public AccountController(
            DatingDbContextEF context,
            ITokenService tokenService,
            IMapper mapper
            )
        {
            _context = context;
            _tokenService = tokenService;
            this.mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.UserName))
            {
                return BadRequest("user is already taken");
            }

            using var hmac = new HMACSHA512();
            var user = mapper.Map<AppUser>(registerDto);

            user.UserName = registerDto.UserName.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
            user.PasswordSalt = hmac.Key;
            user.KnownAs = user.UserName;
            user.City = "Holala";
            user.Country = "Holala";
            user.Created = DateTime.UtcNow;
            user.DateOfBirth = DateOnly.Parse("2024-12-12");
            user.Gender = "Male";
            user.LastActive = DateTime.UtcNow;

            _context.AppUsers.Add(user);
            await _context.SaveChangesAsync();

            var token = _tokenService.GetToken(user);
            var userDto = new UserDto
            { 
                Token = token, 
                Username = user.UserName
            };

            return Ok(userDto);
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

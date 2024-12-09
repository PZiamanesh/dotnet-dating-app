using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GetToken(AppUser user)
        {
            var secret = _config["JwtSettings:Key"];

            if(secret == null || secret.Length < 64) { throw new Exception("can't access to secret key"); }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var userClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.UserName)
            };
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                SigningCredentials = credential,
                Subject = new ClaimsIdentity(userClaims),
                Expires = DateTime.UtcNow.AddDays(7),
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}

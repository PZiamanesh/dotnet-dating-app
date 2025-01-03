using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrrincipalExtensions
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
            string username = 
                user.FindFirstValue(ClaimTypes.NameIdentifier) ?? 
                throw new Exception("Cannot get username from token");

            return username;
        }
    }
}

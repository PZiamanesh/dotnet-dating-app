using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrrincipalExceptions
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

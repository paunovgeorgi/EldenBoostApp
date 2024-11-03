using System.Security.Claims;

namespace EldenBoost.Extenstions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }
    }
}

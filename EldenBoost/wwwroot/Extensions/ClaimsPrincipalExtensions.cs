using System.Security.Claims;
using static EldenBoost.Common.Constants.GeneralApplicationConstants;

namespace EldenBoost.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}

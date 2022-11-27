using System.Security.Claims;

namespace Epsilon.Web.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string Id(this ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;

            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}

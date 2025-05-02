using System.Security.Claims;

namespace DataAccess.Extensions
{
    public static class PrincipalExtensions
    {
        public static string? GetUserEmail(this System.Security.Principal.IPrincipal principal)
        {
            if (!principal.Identity.IsAuthenticated)
            {
                return null;
            }

            string userEmail = (principal.Identity as ClaimsIdentity).FindFirst(f => f.Type == ClaimTypes.Email)?.Value;

            if (!string.IsNullOrEmpty(userEmail))
            {
                return userEmail;
            }

            return null;
        }
    }
}
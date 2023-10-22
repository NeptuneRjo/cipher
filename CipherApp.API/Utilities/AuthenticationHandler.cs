using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace CipherApp.API.Utilities
{
    public static class AuthenticationHandler
    {
        public static async Task Authenticate(HttpContext context, int userId, string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, username)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60 * 12)
            };

            await context.SignInAsync(
                "CookieAuthentication",
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}

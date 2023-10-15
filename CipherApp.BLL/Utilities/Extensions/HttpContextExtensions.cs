using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CipherApp.BLL.Utilities.Extensions
{
    public static class HttpContextExtensions
    {
        public static async Task<ICollection<AuthenticationScheme>> GetExternalProvidersAsync(this HttpContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            var schemes = context.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();

            return (from scheme in await schemes.GetAllSchemesAsync()
                    where !string.IsNullOrEmpty(scheme.DisplayName)
            select scheme).ToList();
        }

        public static async Task<bool> IsProviderSupportedAsync(this HttpContext context, string provider)
        {
            ArgumentNullException.ThrowIfNull(context);

            return (from scheme in await context.GetExternalProvidersAsync()
                    where string.Equals(scheme.Name, provider, StringComparison.OrdinalIgnoreCase)
                    select scheme).Any();
        }
    }
}

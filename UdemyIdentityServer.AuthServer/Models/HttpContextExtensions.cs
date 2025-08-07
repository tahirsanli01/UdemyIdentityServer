using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace UdemyIdentityServer.AuthServer.Models
{
    public static class HttpContextExtensions
    {
        public static async Task<bool> GetSchemeSupportsSignOutAsync(this HttpContext context, string scheme)
        {
            var schemes = context.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();
            var handler = await schemes.GetSchemeAsync(scheme);

            return handler?.HandlerType != null &&
                   typeof(IAuthenticationSignOutHandler).IsAssignableFrom(handler.HandlerType);
        }
    }
}

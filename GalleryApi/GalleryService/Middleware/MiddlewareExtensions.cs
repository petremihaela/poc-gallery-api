using GalleryService.Middleware.TokenAuthorization;
using Microsoft.AspNetCore.Builder;

namespace GalleryService.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenAuthorization(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TokenAuthorizationMiddleware>();
        }
    }
}
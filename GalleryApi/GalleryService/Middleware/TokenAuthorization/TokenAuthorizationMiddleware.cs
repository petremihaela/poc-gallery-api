using GalleryService.Managers;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GalleryService.Middleware.TokenAuthorization
{
    public class TokenAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ITokenManager _tokenManager;

        public TokenAuthorizationMiddleware(RequestDelegate next, ITokenManager tokenManager)
        {
            _next = next;
            _tokenManager = tokenManager;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var token = context.Request.Headers.FirstOrDefault(h => h.Key.Equals("Authorization")).Value;

                var isValidToken = _tokenManager.ValidateToken(token);

                if (!isValidToken)
                    throw new UnauthorizedAccessException();

                await _next.Invoke(context);
            }
            catch (UnauthorizedAccessException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
        }
    }
}
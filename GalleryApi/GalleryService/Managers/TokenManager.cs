using Microsoft.Extensions.Configuration;

namespace GalleryService.Managers
{
    public class TokenManager : ITokenManager
    {
        private readonly IConfiguration _configuration;

        public TokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool ValidateToken(string token)
        {
            return !string.IsNullOrEmpty(token) && token.Equals(_configuration["FakeToken"]);
        }
    }
}
namespace GalleryService.Managers.Token
{
    public interface ITokenManager
    {
        bool ValidateToken(string token);
    }
}
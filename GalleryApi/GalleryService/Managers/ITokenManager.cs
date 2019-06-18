namespace GalleryService.Managers
{
    public interface ITokenManager
    {
        bool ValidateToken(string token);
    }
}
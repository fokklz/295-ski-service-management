namespace SkiServiceAPI.Interfaces
{
    public interface IUserService
    {
        void CreateUser(string username, string password);
        bool VerifyPassword(string username, string password);
    }
}
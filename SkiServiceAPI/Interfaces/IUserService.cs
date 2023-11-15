using SkiServiceAPI.Data;

namespace SkiServiceAPI.Interfaces
{
    public interface IUserService
    {
        void CreateUser(string username, string password, RoleNames role = RoleNames.Mitarbeiter);

        bool VerifyPassword(string username, string password);

        RoleNames GetRole(string username);
    }
}
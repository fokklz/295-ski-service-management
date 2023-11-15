using SkiServiceAPI.Data;

namespace SkiServiceAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(string username, RoleNames role);
    }
}

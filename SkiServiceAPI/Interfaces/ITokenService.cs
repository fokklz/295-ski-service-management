using SkiServiceModels.DTOs;
using SkiServiceModels.Enums;

namespace SkiServiceAPI.Interfaces
{
    public interface ITokenService
    {
        TokenData CreateToken(string id, RoleNames role);
    }
}

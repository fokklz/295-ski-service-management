using SkiServiceAPI.Common;
using SkiServiceModels.DTOs;
using SkiServiceModels.Models;
using SkiServiceModels.Models.EF;

namespace SkiServiceAPI.Interfaces
{
    public interface ITokenService
    {
        Task<TokenData> CreateToken(User user, bool keep = true);
        Task<TaskResult<RefreshResult<User>>> RefreshToken(string token, string refreshToken);
    }
}
using SkiServiceAPI.Common;
using SkiServiceAPI.Models;
using SkiServiceModels.DTOs;
using SkiServiceModels.Models.EF;

namespace SkiServiceAPI.Interfaces
{
    public interface ITokenService
    {
        Task<TokenData> CreateToken(User user, bool keep = true);
        Task<TaskResult<RefreshResult>> RefreshToken(string token, string refreshToken);
    }
}
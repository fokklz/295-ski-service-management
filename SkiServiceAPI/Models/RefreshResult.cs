using SkiServiceModels.DTOs;
using SkiServiceModels.Models.EF;

namespace SkiServiceAPI.Models
{
    public class RefreshResult
    {
        public TokenData TokenData { get; set; }

        public User User { get; set; }
    }
}

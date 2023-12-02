using SkiServiceModels;
using SkiServiceModels.DTOs;

namespace SkiServiceAPI.Models
{
    public class RefreshResult
    {
        public TokenData TokenData { get; set; }

        public User User { get; set; }
    }
}

using SkiServiceModels.DTOs;
using SkiServiceModels.Models.EF;

namespace SkiServiceModels.Models
{
    public class RefreshResult
    {
        public TokenData TokenData { get; set; }

        public User User { get; set; }
    }
}

using SkiServiceModels.Models;
using SkiServiceModels.Models.EF;

namespace SkiServiceAPI.Common
{
    public class LoginResult
    {
        public User? User { get; set; }

        public bool Result { get; set; } = false;
    }
}

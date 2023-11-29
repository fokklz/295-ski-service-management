using SkiServiceModels;

namespace SkiServiceAPI.Common
{
    public class LoginResult
    {
        public User? User { get; set; }

        public bool Result { get; set; } = false;
    }
}

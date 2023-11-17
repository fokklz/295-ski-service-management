using SkiServiceAPI.Models;

namespace SkiServiceAPI.Common
{
    public class LoginResult
    {
        public User? User { get; set; }

        public bool Result { get; set; } = false;
    }
}

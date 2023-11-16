using SkiServiceAPI.Common;

namespace SkiServiceAPI.Interfaces
{
    public interface IValidate
    {
        Task<ValidationResult> ValidateAsync();
    }
}

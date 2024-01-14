using SkiServiceModels.DTOs;
using SkiServiceModels.Interfaces;

namespace SkiServiceModels.Models
{
    /// <summary>
    /// Holds the Result for Refresh operation
    /// </summary>
    /// <typeparam name="T">The user Type to use (since BSON and EF differ)</typeparam>
    public class RefreshResult<T>
        where T : class, IGenericModel
    {
        public TokenData TokenData { get; set; }

        public T User { get; set; }
    }
}

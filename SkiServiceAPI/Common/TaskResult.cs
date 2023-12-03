using SkiServiceModels.DTOs;

namespace SkiServiceAPI.Common
{
    /// <summary>
    /// Reutrn Wrapper for all Task Results inside services to allow for better error handling
    /// and simpler implementation in controllers
    /// </summary>
    /// <typeparam name="T">The actual Response</typeparam>
    public class TaskResult<T> where T : class
    {
        public T? Response { get; private set; }
        public ErrorData ErrorContent { get; private set; }
        public bool IsOk => Response != null;

        public static TaskResult<T> Success(T? response)
        {
            return new TaskResult<T> { Response = response };
        }

        public static TaskResult<T> Error(LocalizationKey key, bool breaking = false)
        {
            return new TaskResult<T> { 
                ErrorContent = new ErrorData
                {
                    MessageCode = key.ToString(),
                    Breaking = breaking
                }
            };
        }
    }


}

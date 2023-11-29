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
        public string Message { get; private set; }
        public bool IsOk => Response != null;

        public static TaskResult<T> Success(T? response)
        {
            return new TaskResult<T> { Response = response };
        }

        public static TaskResult<T> Error(string message)
        {
            return new TaskResult<T> { Message = message };
        }
    }


}

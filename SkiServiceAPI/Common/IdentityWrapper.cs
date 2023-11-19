namespace SkiServiceAPI.Common
{
    /// <summary>
    /// Used as defauklt Wrapper for GenericService to allow the Wrapper type to be used optionally
    /// </summary>
    /// <typeparam name="T">The type to wrap</typeparam>
    public class IdentityWrapper<T>
    {
        public T Value { get; set; }

        public IdentityWrapper(T value)
        {
            Value = value;
        }

        public static implicit operator T(IdentityWrapper<T> wrapper) => wrapper.Value;
        public static implicit operator IdentityWrapper<T>(T value) => new IdentityWrapper<T>(value);
    }

}

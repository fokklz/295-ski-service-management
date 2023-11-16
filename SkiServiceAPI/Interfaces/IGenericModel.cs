namespace SkiServiceAPI.Interfaces
{
    public interface IGenericModel
    {
        int Id { get; set; }

        Task<bool> ValidateAsync();
    }
}

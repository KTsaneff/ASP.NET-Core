namespace CinemaWebApp.Services.Data.Interfaces
{
    public interface IManagerService
    {
        Task<bool> IsUserManagerAsync(string? userId);
    }
}

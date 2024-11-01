namespace CinemaWebApp.Services.Data.Contracts
{
    public interface IBaseService
    {
        bool IsGuidValid(string? id, ref Guid parsedGuid);
    }
}

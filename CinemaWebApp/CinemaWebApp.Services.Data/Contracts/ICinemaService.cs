namespace CinemaWebApp.Services.Data.Contracts
{
    using CinemaWebApp.Web.ViewModels.Cinema;
    public interface ICinemaService
    {
        Task<IEnumerable<CinemaIndexViewModel>> IndexGetAllOrderedByLocationAsync();

        Task AddCinemaAsync(AddCinemaFormModel model);

        Task<CinemaDetailsViewModel?> GetCinemaDetailsByIdAsync(Guid id);

        Task<EditCinemaFormModel?> GetCinemaEditModelByIdAsync(Guid id);

        Task<bool> UpdateCinemaAsync(EditCinemaFormModel model);

        Task<bool> SoftDeleteCinemaAsync(Guid id);
    }
}

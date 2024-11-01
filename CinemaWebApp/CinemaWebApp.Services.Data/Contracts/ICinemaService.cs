namespace CinemaWebApp.Services.Data.Contracts
{
    using CinemaWebApp.Web.ViewModels.Cinema;
    public interface ICinemaService
    {
        Task<IEnumerable<CinemaIndexViewModel>> IndexGetAllOrderedByLocationAsync();

        Task AddCinemaAsync(AddCinemaFormModel model);

        Task<CinemaDetailsViewModel?> GetCinemaDetailsByIdAsync(Guid id);
    }
}

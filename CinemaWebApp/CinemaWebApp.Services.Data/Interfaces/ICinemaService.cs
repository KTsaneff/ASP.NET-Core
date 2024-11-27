namespace CinemaWebApp.Services.Data.Interfaces
{
    using CinemaWebApp.Web.ViewModels.Cinema;

    public interface ICinemaService
    {
        Task<IEnumerable<CinemaIndexViewModel>> IndexGetAllOrderedByLocationAsync();

        Task AddCinemaAsync(AddCinemaFormModel model);

        Task<CinemaDetailsViewModel?> GetCinemaDetailsByIdAsync(Guid id);

        Task<EditCinemaFormModel?> GetCinemaForEditByIdAsync(Guid id);

        Task<bool> EditCinemaAsync(EditCinemaFormModel model);

        Task<CinemaProgramViewModel?> GetCinemaProgramByIdAsync(Guid id);
    }
}
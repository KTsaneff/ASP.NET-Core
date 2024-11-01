namespace CinemaWebApp.Web.ViewModels.Cinema
{
    using CinemaWebApp.Services.Mapping;
    using CinemaWebApp.Data.Models;

    public class CinemaIndexViewModel : IMapFrom<Cinema>
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;
    }
}
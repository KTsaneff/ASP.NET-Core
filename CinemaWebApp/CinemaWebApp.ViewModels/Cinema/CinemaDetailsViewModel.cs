using CinemaWebApp.Web.ViewModels.Movie;

namespace CinemaWebApp.Web.ViewModels.Cinema
{
    public class CinemaDetailsViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public IEnumerable<CinemaMovieViewModel> Movies { get; set; }
            = new HashSet<CinemaMovieViewModel>();
    }
}
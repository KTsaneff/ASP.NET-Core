using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CinemaWebApp.Web.ViewModels.Movie
{
    using CinemaWebApp.Services.Mapping;
    using Data.Models;
    using Services.Mapping;

    public class CinemaMovieViewModel : IMapFrom<Movie>
    {
        public string Title { get; set; } = null!;

        public int Duration { get; set; }
    }
}
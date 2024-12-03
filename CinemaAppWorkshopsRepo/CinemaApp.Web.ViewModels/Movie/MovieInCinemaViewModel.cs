namespace CinemaApp.Web.ViewModels.Movie
{
    using AutoMapper;
    using CinemaApp.Data.Models;
    using CinemaApp.Services.Mapping;

    public class MovieInCinemaViewModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public string Id { get; set; } = null!;
        public string CinemaId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string Duration { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int AvailableTickets { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Movie, MovieInCinemaViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => $"{src.Duration} min"))
                .ForMember(dest => dest.CinemaId, opt => opt.MapFrom(src => GetCinemaId(src)))
                .ForMember(dest => dest.AvailableTickets, opt => opt.MapFrom(src => GetAvailableTickets(src)));
        }

        private static string GetCinemaId(Movie movie)
        {
            var cinema = movie.MovieCinemas.FirstOrDefault();
            return cinema != null ? cinema.CinemaId.ToString() : string.Empty;
        }

        private static int GetAvailableTickets(Movie movie)
        {
            var cinema = movie.MovieCinemas.FirstOrDefault();
            return cinema?.AvailableTickets ?? 0;
        }
    }

}

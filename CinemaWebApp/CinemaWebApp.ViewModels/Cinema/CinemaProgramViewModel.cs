using AutoMapper;
using CinemaWebApp.Web.ViewModels.Movie;
using CinemaWebApp.Services.Mapping;

namespace CinemaWebApp.Web.ViewModels.Cinema
{
    public class CinemaProgramViewModel : IMapFrom<Data.Models.Cinema>, IHaveCustomMappings
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public List<MovieInCinemaViewModel> Movies { get; set; } = new List<MovieInCinemaViewModel>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Data.Models.Cinema, CinemaProgramViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        }
    }
}
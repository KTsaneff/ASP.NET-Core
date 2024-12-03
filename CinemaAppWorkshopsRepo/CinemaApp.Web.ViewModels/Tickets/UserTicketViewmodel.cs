using CinemaApp.Data.Models;
using CinemaApp.Services.Mapping;

using AutoMapper;

namespace CinemaApp.Web.ViewModels.Tickets
{
    public class UserTicketViewModel : IMapFrom<Ticket>
    {
        public Guid TicketId { get; set; }

        public string MovieTitle { get; set; } = null!;

        public string CinemaName { get; set; } = null!;

        public decimal Price { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Ticket, UserTicketViewModel>()
                .ForMember(dest => dest.MovieTitle, opt => opt.MapFrom(src => src.Movie.Title))
                .ForMember(dest => dest.CinemaName, opt => opt.MapFrom(src => src.Cinema.Name));
        }
    }
}

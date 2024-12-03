using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CinemaApp.Data.Models;
using CinemaApp.Services.Mapping;

namespace CinemaApp.Web.ViewModels.Tickets
{
    public class SetAvailableTicketsViewModel : IMapTo<CinemaMovie>
    {
        [Required]
        public Guid CinemaId { get; set; }

        [Required]
        public Guid MovieId { get; set; }

        [Required(ErrorMessage = "Please enter the number of available tickets.")]
        [Range(0, int.MaxValue, ErrorMessage = "Available tickets must be a positive number.")]
        public int AvailableTickets { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<SetAvailableTicketsViewModel, CinemaMovie>()
                .ForMember(dest => dest.AvailableTickets, opt => opt.MapFrom(src => src.AvailableTickets));
        }
    }
}

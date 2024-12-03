using System.ComponentModel.DataAnnotations;
using CinemaApp.Data.Models;
using CinemaApp.Services.Mapping;
using AutoMapper;

namespace CinemaApp.Web.ViewModels.Tickets
{
    public class BuyTicketViewModel : IMapTo<Ticket>
    {
        [Required]
        public Guid CinemaId { get; set; }

        [Required]
        public Guid MovieId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select at least one ticket.")]
        public int Quantity { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<BuyTicketViewModel, Ticket>()
                         .ForMember(t => t.CinemaId, opt => opt.MapFrom(vm => vm.CinemaId))
                         .ForMember(t => t.MovieId, opt => opt.MapFrom(vm => vm.MovieId))
                         .ForMember(t => t.UserId, opt => opt.Ignore());
        }
    }
}

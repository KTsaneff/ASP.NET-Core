namespace CinemaWebApp.Web.ViewModels.Cinema
{
    using Data.Models;
    using Services.Mapping;
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Cinema;

    public class EditCinemaFormModel : IMapTo<Cinema>
    {
        [Required]
        public string Id { get; set; } = null!; // ID to identify the cinema being edited

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(LocationMinLength)]
        [MaxLength(LocationMaxLength)]
        public string Location { get; set; } = null!;
    }
}

﻿namespace CinemaWebApp.Web.ViewModels.Cinema
{
    using Data.Models;
    using Services.Mapping;
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Cinema;

    public class AddCinemaFormModel : IMapTo<Cinema>
    {
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
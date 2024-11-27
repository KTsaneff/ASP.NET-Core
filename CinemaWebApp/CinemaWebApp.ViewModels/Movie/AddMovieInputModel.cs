﻿using static Azure.Core.HttpHeader;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CinemaWebApp.Web.ViewModels.Movie
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using Data.Models;
    using Services.Mapping;

    using static Common.EntityValidationConstants.Movie;
    using static Common.EntityValidationMessages.Movie;

    public class AddMovieInputModel : IMapTo<Movie>, IHaveCustomMappings
    {
        public AddMovieInputModel()
        {
            this.ReleaseDate = DateTime.UtcNow.ToString(ReleaseDateFormat);
        }

        [Required(ErrorMessage = TitleRequiredMessage)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = GenreRequiredMessage)]
        [MinLength(GenreMinLength)]
        [MaxLength(GenreMaxLength)]
        public string Genre { get; set; } = null!;

        [Required(ErrorMessage = ReleaseDateRequiredMessage)]
        public string ReleaseDate { get; set; }

        [Required(ErrorMessage = DurationRequiredMessage)]
        [Range(DurationMinValue, DurationMaxValue)]
        public int Duration { get; set; }

        [Required(ErrorMessage = DirectorRequiredMessage)]
        [MinLength(DirectorNameMinLength)]
        [MaxLength(DirectorNameMaxLength)]
        public string Director { get; set; } = null!;

        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [MaxLength(ImageUrlMaxLength)]
        public string? ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<AddMovieInputModel, Movie>()
                .ForMember(d => d.ReleaseDate, x => x.Ignore());
        }
    }
}
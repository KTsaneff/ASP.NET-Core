﻿namespace CinemaWebApp.Web.ViewModels.Movie
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using CinemaWebApp.Data.Models;
    using CinemaWebApp.Services.Mapping;

    using static Common.EntityValidationConstants.Movie;
    using static Common.EntityValidationMessages.Movie;

    public class EditMovieFormModel : IMapFrom<Movie>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = TitleRequiredMessage)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = GenreRequiredMessage)]
        [MinLength(GenreMinLength)]
        [MaxLength(GenreMaxLength)]
        public string Genre { get; set; } = null!;

        [Required(ErrorMessage = ReleaseDateRequiredMessage)]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

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
            configuration.CreateMap<Movie, EditMovieFormModel>()
                .ForMember(d => d.ReleaseDate, opt => opt.MapFrom(s => s.ReleaseDate));
        }
    }
}

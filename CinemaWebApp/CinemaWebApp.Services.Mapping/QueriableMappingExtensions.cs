﻿using AutoMapper.QueryableExtensions;
using System.Linq.Expressions;

namespace CinemaWebApp.Services.Mapping
{
    public static class QueriableMappingExtensions
    {
        public static IQueryable<TDestination> To<TDestination>(
            this IQueryable source, 
            params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            if(source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ProjectTo(AutoMapperConfig.MapperInstance.ConfigurationProvider, null, membersToExpand);
        }

        public static IQueryable<TDestination> To<TDestination>(
            this IQueryable source, 
            object parameters)
        {
            if(source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ProjectTo<TDestination>(AutoMapperConfig.MapperInstance.ConfigurationProvider, parameters);
        }
    }
}

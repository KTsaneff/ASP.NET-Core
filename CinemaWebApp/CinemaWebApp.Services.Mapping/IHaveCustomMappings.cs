using AutoMapper;

namespace CinemaWebApp.Services.Mapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression congiguration);
    }
}

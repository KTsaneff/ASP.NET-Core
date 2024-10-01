using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RESTful_API_Development_dotNET_Eight.Data;
using RESTful_API_Development_dotNET_Eight.Filters.OprationFilters;

namespace RESTful_API_Development_dotNET_Eight
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ShirtStoreManagement"));
            });

            //Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.OperationFilter<AuthorizationHeaderOperationFilter>();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
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

            builder.Services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                //options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
            });

            builder.Services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
            });
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shirt-Store-Api-V1", Version = "version-1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Shirt-Store-Api-V2", Version = "version-2" });

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
                app.UseSwaggerUI(options =>
                    {
                        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Shirt-Store-Api-V1");
                        options.SwaggerEndpoint("/swagger/v2/swagger.json", "Shirt-Store-Api-V2");
                    });
            }
            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}

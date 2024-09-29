using Microsoft.EntityFrameworkCore;
using RESTful_API_Development_dotNET_Eight.Data;

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

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}

namespace RESTful_API_Development_dotNET_Eight
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add services to the container.

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            //Routing

            // "/shirts"
            app.MapGet("/shirts", () =>
            {
                return "Reading all the shirts...";
            });

            app.MapGet("/shirts/{id}", (int id) =>
            {
                return $"Reading shirt with ID: {id}";
            });

            app.MapPost("/shirts", () =>
            {
                return "Creating a new shirt...";
            });

            app.MapPut("/shirts/{id}", (int id) =>
            {
                return $"Updating shirt with ID: {id}";
            });

            app.MapDelete("/shirts/{id}", (int id) =>
            {
                return $"Deleting shirt with ID: {id}";
            });

            app.Run();
        }
    }
}

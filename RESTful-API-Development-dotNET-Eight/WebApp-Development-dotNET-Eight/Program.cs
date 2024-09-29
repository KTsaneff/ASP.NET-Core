using WebApp_Development_dotNET_Eight.Data;

namespace WebApp_Development_dotNET_Eight
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpClient("Shirts.Api", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7112/api/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<IWebApiExecuter, WebApiExecuter>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

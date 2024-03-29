using Microsoft.EntityFrameworkCore;
using WebNotes.Data;

namespace WebNotes
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<NotesDbContext>(options => options.UseSqlServer(connection));

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            builder.Services.AddControllersWithViews();
            
            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=LoginScreen}/{action=Main}/{id?}");

            app.Run();
        }
    }
}
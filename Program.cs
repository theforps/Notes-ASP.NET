using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebNotes.Data;

namespace WebNotes;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connection = builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"];
        builder.Services.AddDbContext<NotesDbContext>(options => options.UseNpgsql(connection));
        
        builder.Services.AddControllersWithViews();

        builder.Services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => options.LoginPath = "/Authorization/Main");
        builder.Services.AddAuthorization();     
        
        var app = builder.Build();
        
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
            pattern: "{controller=Authorization}/{action=Main}/{id?}");

        app.Run();
    }
}
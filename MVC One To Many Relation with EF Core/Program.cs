using Microsoft.EntityFrameworkCore;
using MVC_One_To_Many_Relation_with_EF_Core.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(opt => {
    opt.UseSqlServer("Server=DESKTOP-0HH3DC0\\SQLEXPRESS;Database=ALLUP-SLIDER;Trusted_Connection=True");

});


var app = builder.Build();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{Controller=home}/{Action=index}");

app.UseStaticFiles();

app.Run();

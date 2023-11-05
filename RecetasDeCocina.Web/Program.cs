using Microsoft.AspNetCore.Identity;
using AspNetCore.Identity.Mongo;
using RecetasDeCocina.Data.Repositories;
using RecetasDeCocina.Data.Models;
using RecetasDeCocina.Logica.Servicios;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<MongoDBRepository>();
builder.Services.AddScoped<IRecetasPersonalizadasServicio, RecetasPersonalizadasServicio>();

/*builder.Services.AddScoped<IUserStore<Usuario>, MongoDBUserStore>();

// Configura Identity para utilizar MongoDB como el almacén de usuarios
builder.Services.AddIdentity<Usuario, IdentityRole>();
// Configura Identity para utilizar MongoDB como el almacén de usuarios

builder.Services.AddIdentity<Usuario, IdentityRole>(
    options =>
    {
        // Configura las opciones de Identity
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
    }
);
 */

var app = builder.Build();

// Configure el pipeline de solicitud HTTP (HTTP request pipeline)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Middleware de autenticación
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

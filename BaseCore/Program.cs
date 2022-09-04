using BaseCore.Entidades;
using BaseCore.Repositorios;
using BaseCore.Servicios;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IVendedoresRepositorio, VendedoresRepositorio>();
builder.Services.AddTransient<IPropiedadesRepositorio, PropiedadesRepositorio>();
builder.Services.AddTransient<IAlmacenadorDeArchivos, AlmacenadorDeArchivos>();
builder.Services.AddTransient<IUsuariosRepositorio, UsuariosRepositorio>();
builder.Services.AddTransient<IUserStore<Usuario>, UsuarioStoreService>();
builder.Services.AddIdentityCore<Usuario>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
});
//builder.Services.AddTransient<IHashService, HashService>();

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

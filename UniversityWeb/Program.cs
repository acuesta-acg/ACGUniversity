using Microsoft.AspNetCore.Authentication.Cookies;
using UniversityWeb.Hubs;
using UniversityWeb.Serv;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddSignalR().AddMessagePackProtocol();

builder.Services.AddHttpClient();
//builder.Services.AddScoped<IServicioApi, ServicioApi>();
builder.Services.AddScoped<IServicioApi, ServicioGRPC>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opciones =>
    {
        opciones.LoginPath = "/Login";
        opciones.LogoutPath = "/Error";
        opciones.AccessDeniedPath = "/Login/AccesoNoPermitido";
    });

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<NuestroHub>("/HubPrueba");

//  https://localhost:7777/HubPrueba


app.Run();

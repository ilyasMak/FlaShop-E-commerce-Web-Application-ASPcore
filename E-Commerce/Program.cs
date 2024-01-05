using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<CUsersPcDesktopProjetsECommerceEcoleMdfContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));

/////////////////////////////////////////////////

builder.Services.AddAuthentication()
    .AddCookie("AdminAuth", options =>
    {
        options.Cookie.Name = "AdminLogin";
        options.LoginPath = "/Administrateur/AdminAuth";

    })
     .AddCookie("ProprietaireAuth", options =>
     {
         options.Cookie.Name = "ProprietaireLogin";
         options.LoginPath = "/Proprietaire/ProprietaireAuth";

     })
      .AddCookie("ClientAuth", options =>
      {
          options.Cookie.Name = "ClientLogin";
          options.LoginPath = "/Client/ClientAuth";

      })


    ;









//////////////////////////////////////////////////
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Client}/{action=Accueil}/{id?}");

app.Run();

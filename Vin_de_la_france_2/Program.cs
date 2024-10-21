using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vin_de_la_france_2.Data;
using Vin_de_la_france_2.Services;
using Microsoft.AspNetCore.Session; 
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString, npgsqlOptions => npgsqlOptions.CommandTimeout(180))); // Timeout ajouté

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false) // false si la confirmation d'email n'est pas nécessaire
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<PanierService>(); 

// **Configuration de la session**
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; 
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// **Utilisation de la session**
app.UseSession();

// Routes pour le contrôleur de compte
app.MapControllerRoute(
    name: "account",
    pattern: "Account/{action=Login}/{id?}",
    defaults: new { controller = "Account" }
);

// Route par défaut pour les articles
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ArticlesClasses}/{action=Index}/{id?}");

// Ajout de Razor Pages si nécessaire
app.MapRazorPages();

app.Run();

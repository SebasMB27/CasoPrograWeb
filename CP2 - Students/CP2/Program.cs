using CP2.Architecture;
using CP2.Core;
using CP2.Data;
using CP2.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile(
    Path.Combine(builder.Environment.ContentRootPath, "..", "CP2.Core", "appsettings.json"),
    optional: true,
    reloadOnChange: true);

// Set the environment variable manually (not recommended for production)
Environment.SetEnvironmentVariable("SECRET_KEY", "E4A1F9B7C32D8F64A9F1C0D3B7E2A6CC4F18B92ED0C4A7F1D3B89C6A5F2E1D44", EnvironmentVariableTarget.Process);

string? secretKey = Environment.GetEnvironmentVariable("SECRECT_KEY");
if (string.IsNullOrWhiteSpace(secretKey))
{
    secretKey = "default";
}

builder.Services.AddSingleton(new SecureHashService(secretKey));

var roomConfigs = builder.Configuration.GetSection("Rooms").Get<List<RoomConfig>>() ?? [];
var roomConfigMap = roomConfigs
    .GroupBy(x => x.Room)
    .ToDictionary(group => group.Key, group => group.Last().Value);
builder.Services.AddSingleton<IReadOnlyDictionary<int, string>>(roomConfigMap);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<FoodbankContext>((serviceProvider, options) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var defaultConnection = configuration.GetConnectionString("DefaultConnection")
                            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    options.UseSqlServer(defaultConnection);
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Intro";
    options.AccessDeniedPath = "/Account/Intro";
});
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddScoped<CP2.Architecture.IRestProvider, CP2.Architecture.RestProvider>();
builder.Services.AddScoped<IRoomsBusiness, RoomsBusiness>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Rooms/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Intro}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();

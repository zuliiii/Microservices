using Microsoft.AspNetCore.Authentication.Cookies;
using System.Configuration;
using Ticket.Shared.Services;
using Ticket.Web.Handler;
using Ticket.Web.Helpers;
using Ticket.Web.Models;
using Ticket.Web.Services;
using Ticket.Web.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);



builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));
builder.Services.AddHttpContextAccessor();
builder.Services.AddAccessTokenManagement();
builder.Services.AddSingleton<PhotoHelper>();
builder.Services.AddScoped<ISharedIdentityService,SharedIdentityService>();

var serviceApiSettings = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();


builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddScoped<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IIdentityService, IdentityService>();
builder.Services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();

builder.Services.AddHttpClient<ICatalogService, CatalogService>(options =>
{
    options.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Catalog.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IPhotoStockService, PhotoStockService>(options =>
{
	options.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.PhotoStock.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();



builder.Services.AddHttpClient<IUserService, UserService>(options =>
{
    options.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUri);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/Auth/SignIn";
        options.ExpireTimeSpan = TimeSpan.FromDays(60);
        options.SlidingExpiration = true;
        options.Cookie.Name = "ticketwebcookie";
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Events}/{action=Create}");

app.Run();

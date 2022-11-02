using ePizzaHub.WebUI.Helpers;
using ePizzaHub.WebUI.Interfaces;
using ePizzaHub.WebUI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Microsoft.Extensions.Azure;
using ePizzaHub.Services.Models;
using ePizzaHub.Services.Configuration;
using ePizzaHub.WebUI.Configuration;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using System;

var builder = WebApplication.CreateBuilder(args);

//logging
builder.Host.UseSerilog((ctx, lc) =>
    lc.ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddTransient<IQueueService, QueueService>();

// Add keyvault services to the container.
builder.Services.AddAzureClients(azureClientFactoryBuilder =>
{
    //help Link: https://learn.microsoft.com/en-us/dotnet/api/overview/azure/identity-readme?view=azure-dotnet
    //Setup Environment Variable
    var VaultUri = new Uri(builder.Configuration["KeyVault:VaultUri"]);
    azureClientFactoryBuilder.AddSecretClient(VaultUri).WithCredential(new DefaultAzureCredential());
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["ConnectionStrings:RedisCache"];
});

ConfigureRepositories.AddServices(builder.Services, builder.Configuration);
ConfigureDependencies.AddServices(builder.Services);

builder.Services.AddScoped<IKeyVaultService, KeyVaultService>();

builder.Services.AddTransient<IQueueService, QueueService>();
builder.Services.AddTransient<IBlobStorageService, BlobStorageService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUserAccessor, UserAccessor>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "epizzahubapp";
        options.LoginPath = new PathString("/account/login");
        options.SlidingExpiration = true;
        options.AccessDeniedPath = new PathString("/account/unauthorize");
    });

builder.Services.Configure<RazorPayConfig>(builder.Configuration.GetSection("RazorPayConfig"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        const int durationInSeconds = 60 * 60 * 24 * 7;
        ctx.Context.Response.Headers["cache-control"] =
            "public, max-age=" + durationInSeconds;
    }
});
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
         );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

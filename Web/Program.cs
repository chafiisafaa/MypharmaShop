using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using Repository.Data;
using Repository.IRepositories;
using Repository.Repositories;
using Repository.UnitOfWork;
using Service.IServices;
using Service.Mapping;
using Service.Services;
using System.ComponentModel.Design;
using Web.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
    // Handling SameSite cookie according to https://docs.microsoft.com/en-us/aspnet/core/security/samesite?view=aspnetcore-3.1
    options.HandleSameSiteCookieCompatibility();
});
// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddDistributedMemoryCache();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"))
    .EnableTokenAcquisitionToCallDownstreamApi(new[] { builder.Configuration["Api:Scopes"] })
    .AddDistributedTokenCaches();



builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Home/Parametrage");
    options.LoginPath = new PathString("/Home/SingOut");
    options.ExpireTimeSpan = TimeSpan.MaxValue;

});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IAuthentificationService, AuthentificationService>();
builder.Services.AddScoped<IPlanningVisiteService, PlanningVisiteService>();



//Repos

builder.Services.AddScoped<IAuthentificationRepository, AuthentificationRepository>();
builder.Services.AddScoped<IPlanningVisiteRepository, PlanningVisiteRepository>();


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.MaxValue;
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
}).AddNewtonsoftJson(options =>
{
    // Configure JSON serialization settings here
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.MaxDepth = 32; // Set your desired depth
                                              // Other JSON serialization settings...
})
    .AddMicrosoftIdentityUI();



builder.Services.AddOptions();
//builder.Services.Configure<OpenIdConnectOptions>(builder.Configuration.GetSection("AzureAdB2C"));
builder.Services.Configure<OpenIdConnectOptions>("OpenIdConnect", options =>
{
    // Event handling
    options.Events = new OpenIdConnectEvents
    {
        OnAuthenticationFailed = context =>
        {
            // Handle authentication failure event
            // You can add your custom logic here
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            // Handle token validation event
            // You can add your custom logic here
            string idtoken = context.TokenEndpointResponse!.IdToken;
            return Task.CompletedTask;
        },
        OnSignedOutCallbackRedirect = context =>
        {
            return Task.CompletedTask;
        }
        // Add more event handlers as needed
    };
});




builder.Services.AddAutoMapper(typeof(MappingProfile));
/*builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    IdentityModelEventSource.ShowPII = true;
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.UseCors("AllowSpecificOrigin");
app.UseEndpoints(endpoints =>
{
    app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Parametrage}/{id?}");
    endpoints.MapRazorPages();
});
//app.UseCors("AllowAnyOrigin");

app.Run();

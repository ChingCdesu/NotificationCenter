using Microsoft.AspNetCore.Authentication.Cookies;
using NotificationCenter.Authentication;
using NotificationCenter.Authentication.Secret;
using NotificationCenter.Components;
using NotificationCenter.Storage;
using NotificationCenter.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<DatabaseContext>()
    .AddSingleton<RedisCache>();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = SecretDefaults.AuthenticationSchema;
    })
    .AddCookie()
    .AddSecret();

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddControllers();

var app = builder.Build();

await app.InitializeDatabase();
app.InitializeAuthentications();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

app.Run();
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ADASOIdentityServer.AuthServer.Repository;
using ADASOIdentityServer.AuthServer.Services;
using ADASOIdentityServer.Database.Contexts;

var builder = WebApplication.CreateBuilder(args);

// === Services ===
builder.Services.AddScoped<ICustomUserRepository, CustomUserRepository>();
builder.Services.AddScoped<AuthDbContext>();

builder.Services.AddIdentityServer()
    .AddClientStore<CustomProfileService>()
    .AddResourceStore<CustomProfileService>()
    //.AddInMemoryApiScopes(Config.GetApiScopes())
    //.AddInMemoryIdentityResources(Config.GetIdentityResources())
    .AddDeveloperSigningCredential()
    .AddProfileService<CustomProfileService>()
    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

builder.Services.AddControllersWithViews();

// === Build uygulama ===
var app = builder.Build();

// === Middleware ===
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

// === Endpoint mapping ===
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// === Uygulamayı başlat ===
app.Run();
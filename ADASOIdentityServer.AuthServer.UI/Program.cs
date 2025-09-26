using ADASOIdentityServer.AuthServer.UI.Helper;
using ADASOIdentityServer.AuthServer.UI.Models;
using ADASOIdentityServer.AuthServer.UI.Services;
using ADASOIdentityServer.Database.Contexts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSingleton<AuthHelper>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

builder.Services.AddAuthentication(opts =>
{
    opts.DefaultScheme = "Cookies";
    opts.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies", opts =>
{
    opts.AccessDeniedPath = "/Home/AccessDenied";
}).AddOpenIdConnect("oidc", opts =>
{
    opts.SignInScheme = "Cookies";
    //opts.Authority = "https://authserver.adasoportal.com/";
    //opts.ClientId = "IdentityUI-Project";
    opts.Authority = "https://localhost:5000/";
    opts.ClientId = "IdentityUI-Test-Project";

    opts.ClientSecret = "K9f!2vG#8xTqP$1bLr7mNzW4dHs6YjQp";
    opts.ResponseType = "code id_token";

    opts.GetClaimsFromUserInfoEndpoint = true;
    opts.SaveTokens = true;

    opts.Scope.Add("api1.read");
    opts.Scope.Add("offline_access");
    opts.Scope.Add("CountryAndCity");
    opts.Scope.Add("email");
    opts.Scope.Add("Roles");
    opts.Scope.Add("userprojects");
    opts.Scope.Add("userprojectsrole");
    opts.Scope.Add("OId");
    opts.ClaimActions.MapUniqueJsonKey("country", "country");
    opts.ClaimActions.MapUniqueJsonKey("city", "city");
    opts.ClaimActions.MapUniqueJsonKey("userprojects", "userprojects");
    opts.ClaimActions.MapUniqueJsonKey("userprojectsrole", "userprojectsrole");
    opts.ClaimActions.MapUniqueJsonKey("role", "role");
    opts.ClaimActions.MapUniqueJsonKey("oid", "oid");

    opts.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        RoleClaimType = "role"
    };

    opts.Events = new OpenIdConnectEvents
    {
        OnRedirectToIdentityProviderForSignOut = context =>
        {
            var idToken = context.Properties.GetTokenValue("id_token");
            var users_ = context.HttpContext.User;

            var projectClaims = JsonSerializer.Deserialize<List<string>>(string.Join(",", context.HttpContext.User.FindAll("userprojects").Select(x => x.Value).ToList()));

            if (!string.IsNullOrEmpty(idToken))
            {
                Console.WriteLine("Logout sırasında id_token_hint: " + idToken);
            }

            return Task.CompletedTask;
        },

        OnRemoteFailure = context =>
        {
            var error = context.Failure?.Message;
            var fullError = context.Failure?.ToString();

            Console.WriteLine("OpenID Connect Hatası: " + fullError);
            context.Response.Redirect("/Home/Error?error=" + Uri.EscapeDataString(error ?? "Bilinmeyen hata"));
            context.HandleResponse(); // varsayılan hata akışını durdur
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ProjectAndRolePolicy", policy =>
        policy.RequireAssertion(context =>
        {
            var user = context.User;
            var hasGlobalRole = user.IsInRole("Admin");

            var projectsClaim = user.FindFirst("userprojects")?.Value;
            if (string.IsNullOrEmpty(projectsClaim))
                return false;

            List<ProjectClaim> projectList;
            try
            {
                projectList = JsonSerializer.Deserialize<List<ProjectClaim>>(projectsClaim) ?? new();
            }
            catch
            {
                return false; // JSON parse hatası varsa reddet
            }

            // örnek kontrol: IdentityUI-Project projesine ait mi?
            var hasProject = projectList.Any(p =>
                p.UserProjects.Equals("IdentityUI-Project", StringComparison.OrdinalIgnoreCase));

            return hasGlobalRole && hasProject;
        }));
});

builder.Services.AddScoped<AuthDbContext>();

var app = builder.Build();

app.UseDeveloperExceptionPage();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
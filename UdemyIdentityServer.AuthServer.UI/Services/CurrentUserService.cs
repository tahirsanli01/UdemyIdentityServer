using Microsoft.EntityFrameworkCore;
using UdemyIdentityServer.AuthServer.UI.Models.Users;
using UdemyIdentityServer.Database.Contexts;

namespace UdemyIdentityServer.AuthServer.UI.Services
{
    public class CurrentUserService(AuthDbContext _authDbContext, IHttpContextAccessor _httpContext) : ICurrentUserService
    {        
        public async Task<UserViewModel> GetCurrentUser()
        {
            var contextUsers = _httpContext.HttpContext.User;

            if (!contextUsers.Identity.IsAuthenticated)
            {
                return new UserViewModel();
            }

            var isAuthenticated = contextUsers.Identity.IsAuthenticated;

            var dBUsers = await _authDbContext.Users
                .Where(x => x.Email == contextUsers.FindFirst("email").Value)
                .Include(x => x.Department)
                .Include(x => x.Role)
                .Include(x => x.UserProjects)
                .ThenInclude(x => x.Project)
                .Include(x => x.PersonelTitle)
                .ToListAsync();


            UserViewModel userViewModel = new UserViewModel()
            {
                UserName = contextUsers.FindFirst("name")?.Value,
                Email = contextUsers.FindFirst("email")?.Value,
                OId = contextUsers.FindFirst("oid")?.Value,
                City = contextUsers.FindFirst("city")?.Value,
                Role = contextUsers.FindFirst("role")?.Value,
                Projects = contextUsers.FindFirst("project")?.Value,
                Departments = dBUsers.FirstOrDefault()?.Department?.Department1 ?? string.Empty,
                Title = dBUsers.FirstOrDefault().PersonelTitle.Title ?? "ADASO"

            };

            return userViewModel;
        }
        
    }
}

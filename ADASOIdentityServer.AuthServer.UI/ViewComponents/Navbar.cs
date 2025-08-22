using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ADASOIdentityServer.AuthServer.UI.Models.Users;
using ADASOIdentityServer.AuthServer.UI.Services;
using ADASOIdentityServer.Database.Contexts;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class Navbar : ViewComponent
    {
        private readonly AuthDbContext _authDbContext;
        private readonly ICurrentUserService _currentUserService;

        public Navbar(AuthDbContext authDbContext, ICurrentUserService currentUserService)
        {
            _authDbContext = authDbContext;
            _currentUserService = currentUserService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var context = HttpContext;
            //var viewContext = ViewContext;
            //var routeData = context.GetRouteData();
            //var actionName = routeData.Values["action"]?.ToString();
            //var controllerName = routeData.Values["controller"]?.ToString();
            //var areaName = routeData.DataTokens["area"]?.ToString();

            UserViewModel userViewModel = await _currentUserService.GetCurrentUser();


            return View("Navbar", userViewModel);
        }
    }
}
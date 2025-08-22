using Microsoft.AspNetCore.Mvc;
using ADASOIdentityServer.AuthServer.UI.Models.Users;
using ADASOIdentityServer.AuthServer.UI.Services;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class DashBoardNavbar : ViewComponent
    {

        private readonly ICurrentUserService _currentUserService;
        public DashBoardNavbar(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            UserViewModel userViewModel = await _currentUserService.GetCurrentUser();

            return View("DashBoardNavbar", userViewModel);
        }
    }
}
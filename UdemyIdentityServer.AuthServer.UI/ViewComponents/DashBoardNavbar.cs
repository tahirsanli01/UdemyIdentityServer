using Microsoft.AspNetCore.Mvc;
using UdemyIdentityServer.AuthServer.UI.Models.Users;
using UdemyIdentityServer.AuthServer.UI.Services;

namespace AdasoAdvisor.Controllers.ViewComponents
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
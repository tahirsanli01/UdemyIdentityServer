using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class MiddleRightMenu : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
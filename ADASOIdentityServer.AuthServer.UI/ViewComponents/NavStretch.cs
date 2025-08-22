using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class NavStretch : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("NavStretch");
        }
    }
}
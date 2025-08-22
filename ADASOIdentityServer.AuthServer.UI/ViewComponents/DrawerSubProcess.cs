using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class DrawerSubProcess : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("DrawerSubProcess");
        }
    }
}
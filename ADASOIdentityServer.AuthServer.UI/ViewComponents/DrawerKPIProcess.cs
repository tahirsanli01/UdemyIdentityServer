using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class DrawerKPIProcess : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("DrawerKPIProcess");
        }
    }
}
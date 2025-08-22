using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class DrawerSetTimingProcess : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("DrawerSetTimingProcess");
        }
    }
}
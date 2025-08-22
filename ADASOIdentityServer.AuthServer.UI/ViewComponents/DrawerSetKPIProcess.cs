using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class DrawerSetKPIProcess : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("DrawerSetKPIProcess");
        }
    }
}
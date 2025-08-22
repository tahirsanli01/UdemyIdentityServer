using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class DrawerMainProcess : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("DrawerMainProcess");
        }
    }
}
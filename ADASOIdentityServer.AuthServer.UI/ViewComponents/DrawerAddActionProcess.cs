using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class DrawerAddActionProcess : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("DrawerAddActionProcess");
        }
    }
}
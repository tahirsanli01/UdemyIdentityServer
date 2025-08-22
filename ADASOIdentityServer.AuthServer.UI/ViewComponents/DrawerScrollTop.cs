using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class DrawerScrollTop : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
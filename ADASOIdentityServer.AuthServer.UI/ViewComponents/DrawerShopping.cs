using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class DrawerShopping : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
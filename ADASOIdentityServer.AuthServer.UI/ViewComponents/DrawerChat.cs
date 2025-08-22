using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class DrawerChat : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class DrawerActivities : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class DrawerDefineCaseProcess : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("DrawerDefineCaseProcess");
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class Footer : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
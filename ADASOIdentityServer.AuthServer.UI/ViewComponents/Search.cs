using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class Search : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
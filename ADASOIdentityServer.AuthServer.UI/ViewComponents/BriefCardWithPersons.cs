using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class BriefCardWithPersons : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
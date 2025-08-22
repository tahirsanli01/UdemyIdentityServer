using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class ProjectStats : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
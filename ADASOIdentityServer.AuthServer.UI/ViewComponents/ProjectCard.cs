using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class ProjectCard : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
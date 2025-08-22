using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class DrawerSurveyProcess : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("DrawerSurveyProcess");
        }
    }
}
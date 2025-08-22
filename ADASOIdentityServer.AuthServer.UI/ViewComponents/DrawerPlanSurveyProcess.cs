using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class DrawerPlanSurveyProcess : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("DrawerPlanSurveyProcess");
        }
    }
}
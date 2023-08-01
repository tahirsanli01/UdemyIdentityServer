using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class DrawerPlanSurveyProcess : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("DrawerPlanSurveyProcess");
        }
    }
}
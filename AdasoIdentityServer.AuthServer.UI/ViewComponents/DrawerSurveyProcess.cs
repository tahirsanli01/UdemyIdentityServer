using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class DrawerSurveyProcess : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("DrawerSurveyProcess");
        }
    }
}
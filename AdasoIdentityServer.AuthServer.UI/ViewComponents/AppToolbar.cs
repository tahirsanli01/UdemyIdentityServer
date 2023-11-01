using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class AppToolbar : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("AppToolbar");
        }
    }
}
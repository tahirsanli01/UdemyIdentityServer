using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class DrawerSetTimingProcess : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("DrawerSetTimingProcess");
        }
    }
}
using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class DrawerKPIProcess : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("DrawerKPIProcess");
        }
    }
}
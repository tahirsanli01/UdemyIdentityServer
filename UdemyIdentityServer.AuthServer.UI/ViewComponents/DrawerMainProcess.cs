using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class DrawerMainProcess : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("DrawerMainProcess");
        }
    }
}
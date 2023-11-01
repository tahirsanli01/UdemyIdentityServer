using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class DrawerSubProcess : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("DrawerSubProcess");
        }
    }
}
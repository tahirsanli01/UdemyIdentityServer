using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class DrawerAddActionProcess : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("DrawerAddActionProcess");
        }
    }
}
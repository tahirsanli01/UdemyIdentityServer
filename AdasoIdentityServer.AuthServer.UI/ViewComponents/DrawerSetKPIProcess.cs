using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class DrawerSetKPIProcess : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("DrawerSetKPIProcess");
        }
    }
}
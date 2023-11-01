using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class DrawerActivities : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
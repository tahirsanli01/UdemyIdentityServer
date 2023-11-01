using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class DrawerScrollTop : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
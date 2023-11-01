using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class DrawerChat : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
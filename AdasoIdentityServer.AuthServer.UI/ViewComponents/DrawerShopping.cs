using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class DrawerShopping : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class Navbar : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
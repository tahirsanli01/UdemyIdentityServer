using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class NavStretch : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("NavStretch");
        }
    }
}
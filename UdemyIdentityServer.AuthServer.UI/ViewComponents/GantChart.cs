using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class GantChart : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
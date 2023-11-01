using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class Search : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
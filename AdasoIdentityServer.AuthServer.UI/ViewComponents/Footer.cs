using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class Footer : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class MiddleRightMenu : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
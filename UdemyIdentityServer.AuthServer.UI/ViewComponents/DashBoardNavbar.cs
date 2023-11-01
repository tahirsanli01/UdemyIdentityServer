using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class DashBoardNavbar : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
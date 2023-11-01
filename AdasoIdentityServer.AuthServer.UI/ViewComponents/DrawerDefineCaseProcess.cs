using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class DrawerDefineCaseProcess : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("DrawerDefineCaseProcess");
        }
    }
}
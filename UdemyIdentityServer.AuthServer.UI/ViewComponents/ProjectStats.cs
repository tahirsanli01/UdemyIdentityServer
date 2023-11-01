using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class ProjectStats : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
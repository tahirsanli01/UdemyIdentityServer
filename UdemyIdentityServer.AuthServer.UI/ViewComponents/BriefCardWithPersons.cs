using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class BriefCardWithPersons : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
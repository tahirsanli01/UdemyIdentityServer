using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class BriefChartCard : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
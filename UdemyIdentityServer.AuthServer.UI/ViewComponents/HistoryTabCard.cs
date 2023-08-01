using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class HistoryTabCard : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
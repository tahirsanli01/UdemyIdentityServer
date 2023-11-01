using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class BestOfferCard : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
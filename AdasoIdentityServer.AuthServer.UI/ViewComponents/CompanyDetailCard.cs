using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class CompanyDetailCard : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("CompanyDetail");
        }
    }
}
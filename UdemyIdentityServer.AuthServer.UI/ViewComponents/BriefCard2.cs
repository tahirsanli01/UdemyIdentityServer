using AdasoAdvisor.Helper;
using AdasoAdvisor.Models.ComponentViewDtos;
using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class BriefCard2 : ViewComponent
    {
        public IViewComponentResult Invoke(BriefCard2Dto model)
        {
            //return View("BriefCard2", model);
            return View("BriefCard2", KPIHelper.Get()) ;
        }
    }
}
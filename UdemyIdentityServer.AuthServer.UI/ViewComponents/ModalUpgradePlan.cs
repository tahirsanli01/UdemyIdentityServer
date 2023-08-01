using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class ModalUpgradePlan : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
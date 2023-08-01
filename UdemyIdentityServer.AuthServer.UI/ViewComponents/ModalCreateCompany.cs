using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class ModalCreateCompany : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("ModalCreateCompany");
        }
    }
}
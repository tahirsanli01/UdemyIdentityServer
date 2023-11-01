using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class ModalNewAddress : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
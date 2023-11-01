using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class ModalViewUsers : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
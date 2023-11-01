using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class ModalCreateProject : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
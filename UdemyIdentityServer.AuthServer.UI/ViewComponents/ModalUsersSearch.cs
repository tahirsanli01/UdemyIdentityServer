using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class ModalUsersSearch : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
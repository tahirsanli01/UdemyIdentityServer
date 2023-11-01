using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class ModalInviteFriends : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
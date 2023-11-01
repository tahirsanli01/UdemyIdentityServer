using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class DrawerSetMeetingProcess : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("DrawerSetMeetingProcess");
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class DrawerSetMeetingProcess : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("DrawerSetMeetingProcess");
        }
    }
}
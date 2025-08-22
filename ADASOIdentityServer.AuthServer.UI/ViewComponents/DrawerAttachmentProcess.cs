using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class DrawerAttachmentProcess : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("DrawerAttachmentProcess");
        }
    }
}
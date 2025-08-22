using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class ModalCreateApp : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
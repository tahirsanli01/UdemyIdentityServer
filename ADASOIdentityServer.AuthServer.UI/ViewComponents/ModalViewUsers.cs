using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class ModalViewUsers : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
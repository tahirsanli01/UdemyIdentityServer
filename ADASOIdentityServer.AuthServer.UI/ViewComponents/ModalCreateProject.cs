using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class ModalCreateProject : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
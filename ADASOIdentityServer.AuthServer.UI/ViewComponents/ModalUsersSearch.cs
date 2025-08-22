using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class ModalUsersSearch : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
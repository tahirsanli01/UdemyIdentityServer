using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class ModalCreateCompany : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("ModalCreateCompany");
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class CompanyDetailCard : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("CompanyDetail");
        }
    }
}
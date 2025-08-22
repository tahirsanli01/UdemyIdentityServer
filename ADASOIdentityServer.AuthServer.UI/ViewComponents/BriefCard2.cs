using ADASOIdentityServer.AuthServer.UI.Helper;
using ADASOIdentityServer.AuthServer.UI.Models.ComponentViewDtos;
using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class BriefCard2 : ViewComponent
    {
        public IViewComponentResult Invoke(BriefCard2Dto model)
        {
            //return View("BriefCard2", model);
            return View("BriefCard2", KPIHelper.Get()) ;
        }
    }
}
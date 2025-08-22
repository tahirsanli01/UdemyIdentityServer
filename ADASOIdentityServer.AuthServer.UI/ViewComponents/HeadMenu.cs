using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class HeadMenu : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            
            return View("HeadMenu");
        }
    }
}
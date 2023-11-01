using Microsoft.AspNetCore.Mvc;

namespace AdasoIdentityServer.AuthServer.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("Users");
        }
    }
}
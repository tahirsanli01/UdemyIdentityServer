using Microsoft.AspNetCore.Mvc;

namespace UdemyIdentityServer.AuthServer.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("Users");
        }
    }
}
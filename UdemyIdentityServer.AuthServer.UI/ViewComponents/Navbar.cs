using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class Navbar : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            var context = HttpContext;
            var viewContext = ViewContext;
            var user = HttpContext.User;
            var isAuthenticated = user;
            var userName = user.Identity.Name; // Genellikle username
            var email = user.FindFirst("email")?.Value; // Email claim’i
            var subId = user.FindFirst("name")?.Value; // Kullanıcı ID

            return View("Navbar");
        }
    }
}
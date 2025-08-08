using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UdemyIdentityServer.AuthServer.UI.Controllers
{
    public class HomeController : Controller
    {

       IHttpContextAccessor _httpContextAccessor;

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        //[Authorize(Roles = "admin")]
        [Authorize]
        public IActionResult Index()
        {
            return Redirect("Users/Index");            
        }

        public IActionResult Login()
        {

            return View();
        }

        //[HttpPost]
 
        //public IActionResult Login([FromForm] string username, string password)
        //{
        //    if (!(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)))
        //    {
        //        if (username == "administrator" && password == "Adaso+-*")
        //        {
        //            _httpContextAccessor.HttpContext?.Session.SetString("username", username);
        //            return Redirect("/Users");
        //        }
        //        else
        //        {
        //            return View("Login", "Error");
        //        }
        //    }
        //    return View();
        //}
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace UdemyIdentityServer.AuthServer.UI.Controllers
{
    public class HomeController : Controller
    {

       IHttpContextAccessor _httpContextAccessor;

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [Authorize]
        public IActionResult Index()
        {
            //return Redirect("Home/Login");

            return View();
        }

        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
 
        public IActionResult Login([FromForm] string username, string password)
        {
            if (!(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)))
            {
                if (username == "administrator" && password == "Adaso+-*")
                {
                    _httpContextAccessor.HttpContext?.Session.SetString("username", username);
                    return Redirect("/Users");
                }
                else
                {
                    return View("Login", "Error");
                }
            }
            return View();
        }
    }
}
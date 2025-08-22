using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ADASOIdentityServer.AuthServer.UI.Controllers
{
    
    public class HomeController : Controller
    {

       IHttpContextAccessor _httpContextAccessor;

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //[Authorize(Roles = "Admin")]

        [Authorize]
        public IActionResult Index()
        {
            // Kullanıcının role ve project claimlerini al
            var roles = User.Claims.Where(x => x.Type == "role").Select(x => x.Value).ToList();
            var projects = User.Claims.Where(x => x.Type == "project").ToList();
            var projectList = JsonSerializer.Deserialize<List<string>>(projects.FirstOrDefault()?.Value ?? "[]");

            //Eğer role veya project yetkisi yoksa AccessDenied'a yönlendir
            if (!roles.Contains("Admin") || !projectList.Any(p => p.Contains("IdentityUI-Project")))
            {
                return RedirectToAction("Login", "Home", new { message = "Bu sayfaya erişim yetkiniz yok." });
            }

            // Yetkiliyse Users/Index sayfasına yönlendir
            return RedirectToAction("Index", "Users");
        }

        public async Task<IActionResult> Login(string message = null)
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
            ViewData["ErrorMessage"] = message ?? "Bu sayfaya erişim yetkiniz yok.";

            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> AccessDenied(string message = null)
        {
            // Token ve project claimlerini al
            var token = await HttpContext.GetTokenAsync("access_token");
            var projectList = User.Claims.Where(x => x.Type == "project").Select(x => x.Value).ToList();

            // Eğer token yok veya proje claimi yoksa → login sayfasına yönlendir
            if (string.IsNullOrEmpty(token) || !projectList.Any(p => p.Contains("IdentityUI-Project")))
            {
                await HttpContext.SignOutAsync("Cookies");
                await HttpContext.SignOutAsync("oidc");

                TempData["ErrorMessage"] = message ?? "Giriş yapmanız gerekiyor veya yetkiniz yok.";

                return Challenge(new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                {
                    RedirectUri = Url.Action("Login", "Home") // Login sonrası gidilecek sayfa
                }, "oidc");
            }

            // Token var ama yetki yoksa → mesaj göster
            ViewData["ErrorMessage"] = message ?? "Bu sayfaya erişim yetkiniz yok.";
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
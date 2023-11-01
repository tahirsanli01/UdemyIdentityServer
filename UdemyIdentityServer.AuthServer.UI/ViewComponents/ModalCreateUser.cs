using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UdemyIdentityServer.Database.Contexts;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class ModalCreateUser : ViewComponent
    {
        private readonly AuthDbContext _context;

        public ModalCreateUser(AuthDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id");
            return View("ModalCreateUser");
        }
    }
}
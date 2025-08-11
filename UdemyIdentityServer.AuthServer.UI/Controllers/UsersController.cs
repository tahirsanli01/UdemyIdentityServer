using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UdemyIdentityServer.AuthServer.UI.Models.ComponentViewDtos;
using UdemyIdentityServer.Database.Contexts;
using UdemyIdentityServer.Database.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UdemyIdentityServer.AuthServer.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly AuthDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(AuthDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            TempData["Users"] = "active";
            var authDbContext = _context.Users.
               Include(u => u.Consultant).
               Include(u => u.Department).
               Include(u => u.PersonelTitle).
               Include(u => u.Role);
            return View(await authDbContext.ToListAsync());
        }



        [HttpPost]
        public async Task<JsonResult> GetUserListAsync([FromBody] PagingDto pagingDto)
        {
            string serachkey = pagingDto.search.value == null ? "" : pagingDto.search.value;

            var query = _context.Users
                .Include(u => u.Consultant)
                .Include(u => u.Department)
                .Include(u => u.PersonelTitle)
                .Include(u => u.Role)
                .Where(x => x.Name.Contains(serachkey));

            var totalCount = await query.CountAsync();

            var pagedData = await query
                .Skip(pagingDto.start)
                .Take(pagingDto.length)
                .Select(u => new
                {
                    u.Id,
                    u.Name,
                    Department = new { u.Department.Department1 },
                    Role = new { u.Role.Name }
                })
                .ToListAsync();

            return Json(new
            {
                draw = pagingDto.draw,
                recordsTotal = totalCount,
                recordsFiltered = pagedData.Count,
                data = pagedData
            });
        }


        // GET: Users/Details/5

        public async Task<IActionResult> Details(int? id)
        {

            TempData["Users"] = "active";

            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.Consultant)
                .Include(u => u.Department)
                .Include(u => u.PersonelTitle)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create

        public IActionResult Create()
        {

            TempData["Users"] = "active";
            ViewData["ConsultantId"] = new SelectList(_context.Consultant, "Id", "Name");
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Department1");
            ViewData["PersonelTitleId"] = new SelectList(_context.PersonelTitle, "Id", "Title");
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");

            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,RoleId,PersonelTitleId,DepartmentId,ConsultantId,Name,Surname,Email,Password,Country,City,Avatar,TobbUyelikOid")] Users users)
        {

            TempData["Users"] = "active";
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ConsultantId"] = new SelectList(_context.Consultant, "Id", "Name", users.ConsultantId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Department1", users.DepartmentId);
            ViewData["PersonelTitleId"] = new SelectList(_context.PersonelTitle, "Id", "Title", users.PersonelTitleId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", users.RoleId);

            return View(users);
        }

        public async Task<IActionResult> Edit(int? id)
        {

            TempData["Users"] = "active";
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            ViewData["ConsultantId"] = new SelectList(_context.Consultant, "Id", "Name", users.ConsultantId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Department1", users.DepartmentId);
            ViewData["PersonelTitleId"] = new SelectList(_context.PersonelTitle, "Id", "Title", users.PersonelTitleId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", users.RoleId);
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoleId,PersonelTitleId,DepartmentId,ConsultantId,Name,Surname,Email,Password,Country,City,Avatar,TobbUyelikOid")] Users users)
        {
            TempData["Users"] = "active";
            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsultantId"] = new SelectList(_context.Consultant, "Id", "Name", users.ConsultantId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Department1", users.DepartmentId);
            ViewData["PersonelTitleId"] = new SelectList(_context.PersonelTitle, "Id", "Title", users.PersonelTitleId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", users.RoleId);
            return View(users);
        }

        // GET: Users/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {

            TempData["Users"] = "active";
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.Consultant)
                .Include(u => u.Department)
                .Include(u => u.PersonelTitle)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            TempData["Users"] = "active";
            if (_context.Users == null)
            {
                return Problem("Entity set 'AuthDbContext.Users'  is null.");
            }
            var users = await _context.Users.FindAsync(id);
            if (users != null)
            {
                _context.Users.Remove(users);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

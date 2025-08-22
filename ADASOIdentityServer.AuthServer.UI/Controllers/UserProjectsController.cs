using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ADASOIdentityServer.Database.Contexts;
using ADASOIdentityServer.Database.Models;

namespace ADASOIdentityServer.AuthServer.UI.Controllers
{
    [Authorize(Policy = "ProjectAndRolePolicy")]
    public class UserProjectsController : Controller
    {
        private readonly AuthDbContext _context;

        public UserProjectsController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: UserProjects
        public async Task<IActionResult> Index()
        {
            
            var authDbContext = _context.UserProjects.Include(u => u.Project).Include(u => u.User);
            TempData["UserProjects"] = "active";
            return View(await authDbContext.ToListAsync());
        }

        // GET: UserProjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserProjects == null)
            {
                return NotFound();
            }

            var userProjects = await _context.UserProjects
                .Include(u => u.Project)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (userProjects == null)
            {
                return NotFound();
            }

            TempData["UserProjects"] = "active";
            return View(userProjects);
        }

        // GET: UserProjects/Create
        public IActionResult Create()
        {
        
            var userList = _context.Users.Select(u => new
            {
                Id = u.Id,
                DisplayText = $"{u.Name} - {u.Email}"
            }).ToList();

            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name");
            ViewData["UserId"] = new SelectList(userList, "Id", "DisplayText");
            TempData["UserProjects"] = "active";
            return View();
        }

        // POST: UserProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ProjectId")] UserProjects userProjects)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userProjects);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", userProjects.ProjectId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", userProjects.UserId);
            TempData["UserProjects"] = "active";
            return View(userProjects);
        }

        // GET: UserProjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserProjects == null)
            {
                return NotFound();
            }

            var userProjects = await _context.UserProjects.FindAsync(id);

            if (userProjects == null)
            {
                return NotFound();
            }
            
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", userProjects.ProjectId);

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", userProjects.UserId);

            TempData["UserProjects"] = "active";
            return View(userProjects);
        }

        // POST: UserProjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ProjectId")] UserProjects userProjects)
        {
            if (id != userProjects.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userProjects);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProjectsExists(userProjects.Id))
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", userProjects.ProjectId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", userProjects.UserId);
            return View(userProjects);
        }

        // GET: UserProjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserProjects == null)
            {
                return NotFound();
            }

            var userProjects = await _context.UserProjects
                .Include(u => u.Project)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userProjects == null)
            {
                return NotFound();
            }
            TempData["UserProjects"] = "active";
            return View(userProjects);
        }

        // POST: UserProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserProjects == null)
            {
                return Problem("Entity set 'AuthDbContext.UserProjects'  is null.");
            }
            var userProjects = await _context.UserProjects.FindAsync(id);
            if (userProjects != null)
            {
                _context.UserProjects.Remove(userProjects);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserProjectsExists(int id)
        {
          return (_context.UserProjects?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

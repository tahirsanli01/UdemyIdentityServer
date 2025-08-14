using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UdemyIdentityServer.Database.Contexts;
using UdemyIdentityServer.Database.Models;

namespace UdemyIdentityServer.AuthServer.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SystemApiResourcesController : Controller
    {
        private readonly AuthDbContext _context;

        public SystemApiResourcesController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: SystemApiResources
        public async Task<IActionResult> Index()
        {
            TempData["SystemApiResources"] = "active";
            var authDbContext = _context.SystemApiResources.Include(s => s.SystemApi);
            return View(await authDbContext.ToListAsync());
        }

        // GET: SystemApiResources/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            TempData["SystemApiResources"] = "active";

            if (id == null || _context.SystemApiResources == null)
            {
                return NotFound();
            }

            var systemApiResources = await _context.SystemApiResources
                .Include(s => s.SystemApi)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemApiResources == null)
            {
                return NotFound();
            }

            return View(systemApiResources);
        }

        // GET: SystemApiResources/Create
        public IActionResult Create()
        {
            TempData["SystemApiResources"] = "active";
            ViewData["SystemApiId"] = new SelectList(_context.SystemApis, "Id", "Id");
            return View();
        }

        // POST: SystemApiResources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SystemApiId,ApiResource,ApiFunction,Explanation")] SystemApiResources systemApiResources)
        {
            if (ModelState.IsValid)
            {
                _context.Add(systemApiResources);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SystemApiId"] = new SelectList(_context.SystemApis, "Id", "Id", systemApiResources.SystemApiId);
            return View(systemApiResources);
        }

        // GET: SystemApiResources/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            TempData["SystemApiResources"] = "active";
            if (id == null || _context.SystemApiResources == null)
            {
                return NotFound();
            }

            var systemApiResources = await _context.SystemApiResources.FindAsync(id);
            if (systemApiResources == null)
            {
                return NotFound();
            }
            ViewData["SystemApiId"] = new SelectList(_context.SystemApis, "Id", "Id", systemApiResources.SystemApiId);
            return View(systemApiResources);
        }

        // POST: SystemApiResources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SystemApiId,ApiResource,ApiFunction,Explanation")] SystemApiResources systemApiResources)
        {
            if (id != systemApiResources.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemApiResources);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemApiResourcesExists(systemApiResources.Id))
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
            ViewData["SystemApiId"] = new SelectList(_context.SystemApis, "Id", "Id", systemApiResources.SystemApiId);
            return View(systemApiResources);
        }

        // GET: SystemApiResources/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            TempData["SystemApiResources"] = "active";
            if (id == null || _context.SystemApiResources == null)
            {
                return NotFound();
            }

            var systemApiResources = await _context.SystemApiResources
                .Include(s => s.SystemApi)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemApiResources == null)
            {
                return NotFound();
            }

            return View(systemApiResources);
        }

        // POST: SystemApiResources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SystemApiResources == null)
            {
                return Problem("Entity set 'AuthDbContext.SystemApiResources'  is null.");
            }
            var systemApiResources = await _context.SystemApiResources.FindAsync(id);
            if (systemApiResources != null)
            {
                _context.SystemApiResources.Remove(systemApiResources);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemApiResourcesExists(int id)
        {
          return (_context.SystemApiResources?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

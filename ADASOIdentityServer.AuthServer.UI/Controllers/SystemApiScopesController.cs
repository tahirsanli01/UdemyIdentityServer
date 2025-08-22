using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ADASOIdentityServer.Database.Contexts;
using ADASOIdentityServer.Database.Models;

namespace ADASOIdentityServer.AuthServer.UI.Controllers
{
    [Authorize(Policy = "ProjectAndRolePolicy")]
    public class SystemApiScopesController : Controller
    {
        private readonly AuthDbContext _context;

        public SystemApiScopesController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: SystemApiScopes
        public async Task<IActionResult> Index()
        {
            TempData["SystemApiScopes"] = "active";
            return _context.SystemApiScopes != null ? 
                          View(await _context.SystemApiScopes.ToListAsync()) :
                          Problem("Entity set 'AuthDbContext.SystemApiScopes'  is null.");
        }

        // GET: SystemApiScopes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            TempData["SystemApiScopes"] = "active";
            if (id == null || _context.SystemApiScopes == null)
            {
                return NotFound();
            }

            var systemApiScopes = await _context.SystemApiScopes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemApiScopes == null)
            {
                return NotFound();
            }

            return View(systemApiScopes);
        }

        // GET: SystemApiScopes/Create
        public IActionResult Create()
        {
            TempData["SystemApiScopes"] = "active";
            return View();
        }

        // POST: SystemApiScopes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApiResource,Scope,Explanation")] SystemApiScopes systemApiScopes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(systemApiScopes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(systemApiScopes);
        }

        // GET: SystemApiScopes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            TempData["SystemApiScopes"] = "active";
            if (id == null || _context.SystemApiScopes == null)
            {
                return NotFound();
            }

            var systemApiScopes = await _context.SystemApiScopes.FindAsync(id);
            if (systemApiScopes == null)
            {
                return NotFound();
            }
            return View(systemApiScopes);
        }

        // POST: SystemApiScopes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ApiResource,Scope,Explanation")] SystemApiScopes systemApiScopes)
        {
            if (id != systemApiScopes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemApiScopes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemApiScopesExists(systemApiScopes.Id))
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
            return View(systemApiScopes);
        }

        // GET: SystemApiScopes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            TempData["SystemApiScopes"] = "active";
            if (id == null || _context.SystemApiScopes == null)
            {
                return NotFound();
            }

            var systemApiScopes = await _context.SystemApiScopes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemApiScopes == null)
            {
                return NotFound();
            }

            return View(systemApiScopes);
        }

        // POST: SystemApiScopes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SystemApiScopes == null)
            {
                return Problem("Entity set 'AuthDbContext.SystemApiScopes'  is null.");
            }
            var systemApiScopes = await _context.SystemApiScopes.FindAsync(id);
            if (systemApiScopes != null)
            {
                _context.SystemApiScopes.Remove(systemApiScopes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemApiScopesExists(int id)
        {
          return (_context.SystemApiScopes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

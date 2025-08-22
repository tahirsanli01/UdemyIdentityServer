using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ADASOIdentityServer.Database.Contexts;
using ADASOIdentityServer.Database.Models;

namespace ADASOIdentityServer.AuthServer.UI.Controllers
{
    [Authorize(Policy = "ProjectAndRolePolicy")]
    public class SystemApisController : Controller
    {
        private readonly AuthDbContext _context;

        public SystemApisController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: SystemApis
        public async Task<IActionResult> Index()
        {
            TempData["SystemApis"] = "active";
            return _context.SystemApis != null ? 
                          View(await _context.SystemApis.ToListAsync()) :
                          Problem("Entity set 'AuthDbContext.SystemApis'  is null.");
        }

        // GET: SystemApis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            TempData["SystemApis"] = "active";
            if (id == null || _context.SystemApis == null)
            {
                return NotFound();
            }

            var systemApis = await _context.SystemApis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemApis == null)
            {
                return NotFound();
            }

            return View(systemApis);
        }

        // GET: SystemApis/Create
        public IActionResult Create()
        {
            TempData["SystemApis"] = "active";
            return View();
        }

        // POST: SystemApis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Explanation,SystemName,ApiSecrets")] SystemApis systemApis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(systemApis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(systemApis);
        }

        // GET: SystemApis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            TempData["SystemApis"] = "active";
            if (id == null || _context.SystemApis == null)
            {
                return NotFound();
            }

            var systemApis = await _context.SystemApis.FindAsync(id);
            if (systemApis == null)
            {
                return NotFound();
            }
            return View(systemApis);
        }

        // POST: SystemApis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Explanation,SystemName,ApiSecrets")] SystemApis systemApis)
        {
            if (id != systemApis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemApis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemApisExists(systemApis.Id))
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
            return View(systemApis);
        }

        // GET: SystemApis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            TempData["SystemApis"] = "active";
            if (id == null || _context.SystemApis == null)
            {
                return NotFound();
            }

            var systemApis = await _context.SystemApis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemApis == null)
            {
                return NotFound();
            }

            return View(systemApis);
        }

        // POST: SystemApis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SystemApis == null)
            {
                return Problem("Entity set 'AuthDbContext.SystemApis'  is null.");
            }
            var systemApis = await _context.SystemApis.FindAsync(id);
            if (systemApis != null)
            {
                _context.SystemApis.Remove(systemApis);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemApisExists(int id)
        {
          return (_context.SystemApis?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

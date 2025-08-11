using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdemyIdentityServer.Database.Contexts;
using UdemyIdentityServer.Database.Models;

namespace UdemyIdentityServer.AuthServer.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SystemIdentityRosourcesController : Controller
    {
        private readonly AuthDbContext _context;

        public SystemIdentityRosourcesController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: SystemIdentityRosources
        public async Task<IActionResult> Index()
        {
              return _context.SystemIdentityRosources != null ? 
                          View(await _context.SystemIdentityRosources.ToListAsync()) :
                          Problem("Entity set 'AuthDbContext.SystemIdentityRosources'  is null.");
        }

        // GET: SystemIdentityRosources/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SystemIdentityRosources == null)
            {
                return NotFound();
            }

            var systemIdentityRosources = await _context.SystemIdentityRosources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemIdentityRosources == null)
            {
                return NotFound();
            }

            return View(systemIdentityRosources);
        }

        // GET: SystemIdentityRosources/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SystemIdentityRosources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DisplayName,Explanation,UserClaims")] SystemIdentityRosources systemIdentityRosources)
        {
            if (ModelState.IsValid)
            {
                _context.Add(systemIdentityRosources);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(systemIdentityRosources);
        }

        // GET: SystemIdentityRosources/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SystemIdentityRosources == null)
            {
                return NotFound();
            }

            var systemIdentityRosources = await _context.SystemIdentityRosources.FindAsync(id);
            if (systemIdentityRosources == null)
            {
                return NotFound();
            }
            return View(systemIdentityRosources);
        }

        // POST: SystemIdentityRosources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DisplayName,Explanation,UserClaims")] SystemIdentityRosources systemIdentityRosources)
        {
            if (id != systemIdentityRosources.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemIdentityRosources);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemIdentityRosourcesExists(systemIdentityRosources.Id))
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
            return View(systemIdentityRosources);
        }

        // GET: SystemIdentityRosources/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SystemIdentityRosources == null)
            {
                return NotFound();
            }

            var systemIdentityRosources = await _context.SystemIdentityRosources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemIdentityRosources == null)
            {
                return NotFound();
            }

            return View(systemIdentityRosources);
        }

        // POST: SystemIdentityRosources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SystemIdentityRosources == null)
            {
                return Problem("Entity set 'AuthDbContext.SystemIdentityRosources'  is null.");
            }
            var systemIdentityRosources = await _context.SystemIdentityRosources.FindAsync(id);
            if (systemIdentityRosources != null)
            {
                _context.SystemIdentityRosources.Remove(systemIdentityRosources);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemIdentityRosourcesExists(int id)
        {
          return (_context.SystemIdentityRosources?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

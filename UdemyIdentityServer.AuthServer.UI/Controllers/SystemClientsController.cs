using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdemyIdentityServer.Database.Contexts;
using UdemyIdentityServer.Database.Models;

namespace UdemyIdentityServer.AuthServer.UI.Controllers
{
    
    [Authorize(Roles = "Admin")]

    public class SystemClientsController : Controller
    {
        private readonly AuthDbContext _context;

        public SystemClientsController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: SystemClients
        public async Task<IActionResult> Index()
        {
              return _context.SystemClients != null ? 
                          View(await _context.SystemClients.ToListAsync()) :
                          Problem("Entity set 'AuthDbContext.SystemClients'  is null.");
        }

        // GET: SystemClients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SystemClients == null)
            {
                return NotFound();
            }

            var systemClients = await _context.SystemClients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemClients == null)
            {
                return NotFound();
            }

            return View(systemClients);
        }

        // GET: SystemClients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SystemClients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,RequirePkce,ClientName,ClientSecrets,AllowedGrantTypes,AllowedGrantTypeExplanation,RedirectUris,PostLogoutRedirectUris,AccessTokenLifetime,AllowOfflineAccess,RefreshTokenUsage,RefreshTokenUsageExplanation,RefreshTokenExpiration,RefreshTokenExpirationExplanation,AbsoluteRefreshTokenLifetime,RequireConsent")] SystemClients systemClients)
        {
            if (ModelState.IsValid)
            {
                _context.Add(systemClients);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(systemClients);
        }

        // GET: SystemClients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SystemClients == null)
            {
                return NotFound();
            }

            var systemClients = await _context.SystemClients.FindAsync(id);
            if (systemClients == null)
            {
                return NotFound();
            }
            return View(systemClients);
        }

        // POST: SystemClients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,RequirePkce,ClientName,ClientSecrets,AllowedGrantTypes,AllowedGrantTypeExplanation,RedirectUris,PostLogoutRedirectUris,AccessTokenLifetime,AllowOfflineAccess,RefreshTokenUsage,RefreshTokenUsageExplanation,RefreshTokenExpiration,RefreshTokenExpirationExplanation,AbsoluteRefreshTokenLifetime,RequireConsent")] SystemClients systemClients)
        {
            if (id != systemClients.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemClients);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemClientsExists(systemClients.Id))
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
            return View(systemClients);
        }

        // GET: SystemClients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SystemClients == null)
            {
                return NotFound();
            }

            var systemClients = await _context.SystemClients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemClients == null)
            {
                return NotFound();
            }

            return View(systemClients);
        }

        // POST: SystemClients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SystemClients == null)
            {
                return Problem("Entity set 'AuthDbContext.SystemClients'  is null.");
            }
            var systemClients = await _context.SystemClients.FindAsync(id);
            if (systemClients != null)
            {
                _context.SystemClients.Remove(systemClients);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemClientsExists(int id)
        {
          return (_context.SystemClients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UdemyIdentityServer.Database.Contexts;
using UdemyIdentityServer.Database.Models;

namespace UdemyIdentityServer.AuthServer.UI.Controllers
{
    [Authorize(Policy = "ProjectAndRolePolicy")]
    public class SystemClientAllowedScopesController : Controller
    {
        private readonly AuthDbContext _context;

        public SystemClientAllowedScopesController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: SystemClientAllowedScopes
        public async Task<IActionResult> Index()
        {
            TempData["SystemClientAllowedScopes"] = "active";
            var authDbContext = _context.SystemClientAllowedScopes.Include(s => s.SystemClient);
            return View(await authDbContext.ToListAsync());
        }

        // GET: SystemClientAllowedScopes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            TempData["SystemClientAllowedScopes"] = "active";
            if (id == null || _context.SystemClientAllowedScopes == null)
            {
                return NotFound();
            }

            var systemClientAllowedScopes = await _context.SystemClientAllowedScopes
                .Include(s => s.SystemClient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemClientAllowedScopes == null)
            {
                return NotFound();
            }

            return View(systemClientAllowedScopes);
        }

        // GET: SystemClientAllowedScopes/Create
        public IActionResult Create()
        {
            TempData["SystemClientAllowedScopes"] = "active";
            ViewData["SystemClientId"] = new SelectList(_context.SystemClients, "Id", "ClientId");
            return View();
        }

        // POST: SystemClientAllowedScopes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SystemClientId,Name,Explanation")] SystemClientAllowedScopes systemClientAllowedScopes)
        {
            TempData["SystemClientAllowedScopes"] = "active";
            if (ModelState.IsValid)
            {
                _context.Add(systemClientAllowedScopes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SystemClientId"] = new SelectList(_context.SystemClients, "Id", "ClientId", systemClientAllowedScopes.SystemClientId);
            return View(systemClientAllowedScopes);
        }

        // GET: SystemClientAllowedScopes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            TempData["SystemClientAllowedScopes"] = "active";

            if (id == null || _context.SystemClientAllowedScopes == null)
            {
                return NotFound();
            }

            var systemClientAllowedScopes = await _context.SystemClientAllowedScopes.FindAsync(id);
            if (systemClientAllowedScopes == null)
            {
                return NotFound();
            }
            ViewData["SystemClientId"] = new SelectList(_context.SystemClients, "Id", "ClientId", systemClientAllowedScopes.SystemClientId);
            return View(systemClientAllowedScopes);
        }

        // POST: SystemClientAllowedScopes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SystemClientId,Name,Explanation")] SystemClientAllowedScopes systemClientAllowedScopes)
        {
            if (id != systemClientAllowedScopes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemClientAllowedScopes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemClientAllowedScopesExists(systemClientAllowedScopes.Id))
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
            ViewData["SystemClientId"] = new SelectList(_context.SystemClients, "Id", "ClientId", systemClientAllowedScopes.SystemClientId);
            return View(systemClientAllowedScopes);
        }
        // GET: SystemClientAllowedScopes/Copy create view
        // GET: SystemClientAllowedScopes/Copy/5

        public async Task<IActionResult> Copy()
        {
            var systemClients = await _context.SystemClients.ToListAsync();
            ViewBag.SystemClients = new SelectList(systemClients, "Id", "ClientId");

            return View(new CopyViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Copy(CopyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var systemClients = await _context.SystemClients.ToListAsync();
                ViewBag.SystemClients = new SelectList(systemClients, "Id", "ClientId");
                return View(model);
            }

            //Copy
            var sourceScopes = await _context.SystemClientAllowedScopes
                .Where(s => s.SystemClientId == model.SourceSystemClientId)
                .ToListAsync();
            
            if (sourceScopes == null || !sourceScopes.Any())
            {
                ModelState.AddModelError("", "Kaynak istemci için izin verilen kapsam bulunamadı.");
                var systemClients = await _context.SystemClients.ToListAsync();
                ViewBag.SystemClients = new SelectList(systemClients, "Id", "ClientId");
                return View(model);
            }
            foreach (var scope in sourceScopes)
            {
                var systemClient = await _context.SystemClients.FindAsync(model.TargetSystemClientId);


                var newScope = new SystemClientAllowedScopes
                {
                    SystemClientId = model.TargetSystemClientId,
                    Name = scope.Name,
                    Explanation = scope.Explanation + $"ClientId : {model.SourceSystemClientId}- {systemClient.ClientName}' dan kopyalandı",
                };
                _context.SystemClientAllowedScopes.Add(newScope);

                _context.SaveChanges();



            }

            // İşlem başarılıysa yönlendirme
            return RedirectToAction("Index");
        }




        // GET: SystemClientAllowedScopes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            TempData["SystemClientAllowedScopes"] = "active";
            if (id == null || _context.SystemClientAllowedScopes == null)
            {
                return NotFound();
            }

            var systemClientAllowedScopes = await _context.SystemClientAllowedScopes
                .Include(s => s.SystemClient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemClientAllowedScopes == null)
            {
                return NotFound();
            }

            return View(systemClientAllowedScopes);
        }

        // POST: SystemClientAllowedScopes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SystemClientAllowedScopes == null)
            {
                return Problem("Entity set 'AuthDbContext.SystemClientAllowedScopes'  is null.");
            }
            var systemClientAllowedScopes = await _context.SystemClientAllowedScopes.FindAsync(id);
            if (systemClientAllowedScopes != null)
            {
                _context.SystemClientAllowedScopes.Remove(systemClientAllowedScopes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemClientAllowedScopesExists(int id)
        {
          return (_context.SystemClientAllowedScopes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

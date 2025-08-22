using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ADASOIdentityServer.Database.Contexts;
using ADASOIdentityServer.Database.Models;

namespace ADASOIdentityServer.AuthServer.UI.Controllers
{
    [Authorize(Policy = "ProjectAndRolePolicy")]
    public class PersonelTitlesController : Controller
    {
        private readonly AuthDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PersonelTitlesController(AuthDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
 
        public async Task<IActionResult> Index()
        {
        
            TempData["PersonelTitles"] = "active";
              return _context.PersonelTitle != null ? 
                          View(await _context.PersonelTitle.ToListAsync()) :
                          Problem("Entity set 'AuthDbContext.PersonelTitle'  is null.");
        }

        // GET: PersonelTitles/Details/5
        public async Task<IActionResult> Details(int? id)
        { 
            TempData["PersonelTitles"] = "active";
            if (id == null || _context.PersonelTitle == null)
            {
                return NotFound();
            }

            var personelTitle = await _context.PersonelTitle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personelTitle == null)
            {
                return NotFound();
            }

            return View(personelTitle);
        }

        // GET: PersonelTitles/Create
        public IActionResult Create()
        { 
            TempData["PersonelTitles"] = "active";
            return View();
        }

        // POST: PersonelTitles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] PersonelTitle personelTitle)
        {
            TempData["PersonelTitles"] = "active";
            if (ModelState.IsValid)
            {
                _context.Add(personelTitle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personelTitle);
        }

        // GET: PersonelTitles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        { 
            TempData["PersonelTitles"] = "active";
            if (id == null || _context.PersonelTitle == null)
            {
                return NotFound();
            }

            var personelTitle = await _context.PersonelTitle.FindAsync(id);
            if (personelTitle == null)
            {
                return NotFound();
            }
            return View(personelTitle);
        }

        // POST: PersonelTitles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] PersonelTitle personelTitle)
        {
            TempData["PersonelTitles"] = "active";
            if (id != personelTitle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personelTitle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonelTitleExists(personelTitle.Id))
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
            return View(personelTitle);
        }

        // GET: PersonelTitles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        { 
            TempData["PersonelTitles"] = "active";
            if (id == null || _context.PersonelTitle == null)
            {
                return NotFound();
            }

            var personelTitle = await _context.PersonelTitle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personelTitle == null)
            {
                return NotFound();
            }

            return View(personelTitle);
        }

        // POST: PersonelTitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            TempData["PersonelTitles"] = "active";
            if (_context.PersonelTitle == null)
            {
                return Problem("Entity set 'AuthDbContext.PersonelTitle'  is null.");
            }
            var personelTitle = await _context.PersonelTitle.FindAsync(id);

            if (personelTitle != null)
            {
                _context.PersonelTitle.Remove(personelTitle);
            }
            
            
            _context.Remove(personelTitle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonelTitleExists(int id)
        {
          return (_context.PersonelTitle?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

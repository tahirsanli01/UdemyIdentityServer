using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UdemyIdentityServer.Database.Contexts;
using UdemyIdentityServer.Database.Models;

namespace UdemyIdentityServer.AuthServer.UI.Controllers
{
    public class PersonelTitlesController : Controller
    {
        private readonly AuthDbContext _context;

        public PersonelTitlesController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: PersonelTitles
        public async Task<IActionResult> Index()
        {
              return _context.PersonelTitle != null ? 
                          View(await _context.PersonelTitle.ToListAsync()) :
                          Problem("Entity set 'AuthDbContext.PersonelTitle'  is null.");
        }

        // GET: PersonelTitles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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
            return View();
        }

        // POST: PersonelTitles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] PersonelTitle personelTitle)
        {
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
            if (_context.PersonelTitle == null)
            {
                return Problem("Entity set 'AuthDbContext.PersonelTitle'  is null.");
            }
            var personelTitle = await _context.PersonelTitle.FindAsync(id);
            if (personelTitle != null)
            {
                _context.PersonelTitle.Remove(personelTitle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonelTitleExists(int id)
        {
          return (_context.PersonelTitle?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

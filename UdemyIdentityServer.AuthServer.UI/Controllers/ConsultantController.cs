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
    public class ConsultantController : Controller
    {
        private readonly AuthDbContext _context;

        public ConsultantController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: Consultant
        public async Task<IActionResult> Index()
        {
            TempData["Consultant"] = "active";
            return _context.Consultant != null ? 
                          View(await _context.Consultant.ToListAsync()) :
                          Problem("Entity set 'AuthDbContext.Consultant'  is null.");
        }

        // GET: Consultant/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            TempData["Consultant"] = "active";
            if (id == null || _context.Consultant == null)
            {
                return NotFound();
            }

            var consultant = await _context.Consultant
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultant == null)
            {
                return NotFound();
            }

            return View(consultant);
        }

        // GET: Consultant/Create
        public IActionResult Create()
        {
            TempData["Consultant"] = "active";
            return View();
        }

        // POST: Consultant/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Consultant consultant)
        {
            TempData["Consultant"] = "active";
            if (ModelState.IsValid)
            {
                _context.Add(consultant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consultant);
        }

        // GET: Consultant/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            TempData["Consultant"] = "active";
            if (id == null || _context.Consultant == null)
            {
                return NotFound();
            }

            var consultant = await _context.Consultant.FindAsync(id);
            if (consultant == null)
            {
                return NotFound();
            }
            return View(consultant);
        }

        // POST: Consultant/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Consultant consultant)
        {
            TempData["Consultant"] = "active";
            if (id != consultant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultantExists(consultant.Id))
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
            return View(consultant);
        }

        // GET: Consultant/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            TempData["Consultant"] = "active";
            if (id == null || _context.Consultant == null)
            {
                return NotFound();
            }

            var consultant = await _context.Consultant
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultant == null)
            {
                return NotFound();
            }

            return View(consultant);
        }

        // POST: Consultant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            TempData["Consultant"] = "active";
            if (_context.Consultant == null)
            {
                return Problem("Entity set 'AuthDbContext.Consultant'  is null.");
            }
            var consultant = await _context.Consultant.FindAsync(id);
            if (consultant != null)
            {
                _context.Consultant.Remove(consultant);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultantExists(int id)
        {
          return (_context.Consultant?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

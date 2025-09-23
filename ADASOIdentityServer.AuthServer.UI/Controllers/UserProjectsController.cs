using ADASOIdentityServer.AuthServer.UI.Models.UserProject;
using ADASOIdentityServer.Database.Contexts;
using ADASOIdentityServer.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

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

            var authDbContext = _context.UserProjects
                                        .Include(u => u.Project)
                                        .Include(u => u.User)
                                        .Include(u => u.UserProjectRole);

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

            //var roles = Enum.GetValues(typeof(ProjectRole))
            //.Cast<ProjectRole>()
            //.Select(r => new { Id = (int)r, Name = r.ToString() });

            var roles = _context.ProjectRole;

            ViewData["UserProjectRole"] = new SelectList(roles, "Id", "Name");

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
        public async Task<IActionResult> Create([Bind("Id,UserId,ProjectId,SelectedRoleIds")] UserProjects userProjects)
        {
            if (ModelState.IsValid)
            {
                if (userProjects.SelectedRoleIds != null && userProjects.SelectedRoleIds.Count > 0)
                {
                    foreach (var roleId in userProjects.SelectedRoleIds)
                    {
                        var userProjectRole = new UserProjectRole
                        {
                            UserProjects = userProjects,
                            Id = roleId                 
                        };
                        _context.UserProjectRole.Add(userProjectRole);
                    }
                }
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

            // Projeye atanmış roller
            var projectRoles = _context.UserProjectRole
                .Where(r => r.UserProjectsId == userProjects.Id)
                .Select(r => r.ProjectRoleId)
                .ToList();

            // Enum'dan roller
            var roles = _context.ProjectRole;

            // MultiSelectList (seçilmiş roller ile)
            ViewData["SelectedRoleIds"] = new MultiSelectList(roles, "Id", "Name", projectRoles);

            // Diğer dropdownlar
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", userProjects.ProjectId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", userProjects.UserId);

            TempData["UserProjects"] = "active";

            // ViewModel doldur
            var vm = new UserProjectEditViewModel
            {
                UserProject = userProjects,
                SelectedRoleIds = projectRoles
            };

            return View(vm);
        }


        // POST: UserProjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserProjectEditPostModel userProjects)
        {
            if (id != userProjects.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingUserProject = await _context.UserProjects
                    .Include(up => up.UserProjectRole)
                    .FirstOrDefaultAsync(up => up.Id == userProjects.Id);

                if (existingUserProject == null)
                {
                    return NotFound();
                }

                try
                {
                    // 1) UserProject tablosunu güncelle
                    existingUserProject.UserId = userProjects.UserId;
                    existingUserProject.ProjectId = userProjects.ProjectId;

                    
                    await _context.SaveChangesAsync();



                    // 2) Roller güncelle
                    _context.UserProjectRole.RemoveRange(existingUserProject.UserProjectRole);

                    if (userProjects.SelectedRoleIds != null && userProjects.SelectedRoleIds.Count > 0)
                    {
                        foreach (var roleId in userProjects.SelectedRoleIds)
                        {
                            var userProjectRole = new UserProjectRole
                            {
                                UserProjectsId = userProjects.Id,
                                ProjectRoleId = roleId
                            };
                            _context.UserProjectRole.Add(userProjectRole);
                        }
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.UserProjects.Any(e => e.Id == userProjects.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
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
                return NotFound();


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
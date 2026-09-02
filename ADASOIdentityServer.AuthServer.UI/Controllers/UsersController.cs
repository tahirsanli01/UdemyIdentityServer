using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ADASOIdentityServer.Database.Contexts;
using ADASOIdentityServer.Database.Models;
using ADASOIdentityServer.AuthServer.UI.Models.Users;

namespace ADASOIdentityServer.AuthServer.UI.Controllers
{
    [Authorize(Policy = "ProjectAndRolePolicy")]
    public class UsersController : Controller
    {
        private readonly AuthDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(AuthDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index(string? search, int? userTypeId, bool unassigned = false, int page = 1, int pageSize = 25)
        {
            TempData["Users"] = "active";
            ViewBag.Breadcrumb = new List<string> { "Kullanıcı Yönetimi", "Kullanıcı Listesi" };
            page = Math.Max(1, page);
            pageSize = new[] { 10, 25, 50, 100 }.Contains(pageSize) ? pageSize : 25;
            search = search?.Trim();

            var query = _context.Users.AsNoTracking();
            var totalCount = await query.CountAsync();
            var typeCounts = await query
                .GroupBy(x => x.UserTypeId)
                .Select(g => new UserTypeCountViewModel { UserTypeId = g.Key, Count = g.Count() })
                .ToListAsync();

            if (unassigned)
                query = query.Where(x => x.UserTypeId == null);
            else if (userTypeId.HasValue)
                query = query.Where(x => x.UserTypeId == userTypeId.Value);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(x => x.Name.Contains(search) || x.Surname.Contains(search) || x.Email.Contains(search));

            var filteredCount = await query.CountAsync();
            var totalPages = Math.Max(1, (int)Math.Ceiling(filteredCount / (double)pageSize));
            page = Math.Min(page, totalPages);

            var users = await query
                .OrderBy(x => x.Name).ThenBy(x => x.Surname)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .Select(x => new UserListRowViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Email = x.Email,
                    Department = x.UserType != null && x.UserType.Code == "PERSONEL" ? x.Department.Department1 : null,
                    Title = x.UserType != null && x.UserType.Code == "PERSONEL" ? x.PersonelTitle.Title : null,
                    Role = x.Role.Name,
                    UserTypeId = x.UserTypeId,
                    UserTypeName = x.UserType == null ? null : x.UserType.Name,
                    UserTypeCode = x.UserType == null ? null : x.UserType.Code,
                    IsActive = x.IsActive != false
                }).ToListAsync();

            var model = new UsersIndexViewModel
            {
                Users = users,
                UserTypes = await _context.UserType.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.SortOrder).ToListAsync(),
                TypeCounts = typeCounts,
                Search = search ?? string.Empty,
                UserTypeId = userTypeId,
                Unassigned = unassigned,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                FilteredCount = filteredCount
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UserProjectsModal(int id)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return NotFound();

            var assignments = await _context.UserProjects
                .AsNoTracking()
                .Include(x => x.Project)
                .Include(x => x.UserProjectRole)
                    .ThenInclude(x => x.ProjectRole)
                .Where(x => x.UserId == id)
                .OrderBy(x => x.Project.Name)
                .ToListAsync();

            var model = new UserProjectsModalViewModel
            {
                UserId = id,
                UserDisplayName = $"{user.Name} {user.Surname}".Trim(),
                Assignments = assignments,
                Projects = new SelectList(await _context.Projects.AsNoTracking().OrderBy(x => x.Name).ToListAsync(), "Id", "Name"),
                Roles = new MultiSelectList(await _context.ProjectRole.AsNoTracking().OrderBy(x => x.Name).ToListAsync(), "Id", "Name")
            };

            return PartialView("_UserProjectsModal", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserProject(int userId, int projectId, List<int> selectedRoleIds)
        {
            if (!await _context.Users.AnyAsync(x => x.Id == userId) ||
                !await _context.Projects.AnyAsync(x => x.Id == projectId))
                return BadRequest(new { message = "Kullanıcı veya proje bulunamadı." });

            if (await _context.UserProjects.AnyAsync(x => x.UserId == userId && x.ProjectId == projectId))
                return BadRequest(new { message = "Kullanıcı bu projeye zaten atanmış." });

            var userProject = new UserProjects { UserId = userId, ProjectId = projectId };
            foreach (var roleId in selectedRoleIds.Distinct())
                userProject.UserProjectRole.Add(new UserProjectRole { ProjectRoleId = roleId });

            _context.UserProjects.Add(userProject);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Proje ve yetkiler kullanıcıya eklendi." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserProject(int id)
        {
            var userProject = await _context.UserProjects
                .Include(x => x.UserProjectRole)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (userProject == null)
                return NotFound(new { message = "Kullanıcının proje ataması bulunamadı." });

            _context.UserProjectRole.RemoveRange(userProject.UserProjectRole);
            _context.UserProjects.Remove(userProject);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                userId = userProject.UserId,
                message = "Proje ataması ve bağlı yetkiler silindi."
            });
        }


        // GET: Users/Details/5

        public async Task<IActionResult> Details(int? id)
        {

            TempData["Users"] = "active";

            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.Consultant)
                .Include(u => u.Department)
                .Include(u => u.PersonelTitle)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create

        public IActionResult Create()
        {

            TempData["Users"] = "active";
            ViewData["ConsultantId"] = new SelectList(_context.Consultant, "Id", "Name");
            ViewData["DepartmentId"] = GetDepartmentSelectList();
            ViewData["PersonelTitleId"] = new SelectList(_context.PersonelTitle, "Id", "Title");
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            ViewData["UserTypeId"] = new SelectList(_context.UserType.Where(x => x.IsActive).OrderBy(x => x.SortOrder), "Id", "Name");
            ViewData["PersonnelUserTypeId"] = _context.UserType.Where(x => x.Code == "PERSONEL").Select(x => (int?)x.Id).FirstOrDefault();

            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,RoleId,PersonelTitleId,DepartmentId,ConsultantId,UserTypeId,IsActive,Name,Surname,Email,Password,Country,City,Avatar,TobbUyelikOid")] Users users)
        {

            TempData["Users"] = "active";
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ConsultantId"] = new SelectList(_context.Consultant, "Id", "Name", users.ConsultantId);
            ViewData["DepartmentId"] = GetDepartmentSelectList(users.DepartmentId);
            ViewData["PersonelTitleId"] = new SelectList(_context.PersonelTitle, "Id", "Title", users.PersonelTitleId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", users.RoleId);
            ViewData["UserTypeId"] = new SelectList(_context.UserType.Where(x => x.IsActive).OrderBy(x => x.SortOrder), "Id", "Name", users.UserTypeId);
            ViewData["PersonnelUserTypeId"] = _context.UserType.Where(x => x.Code == "PERSONEL").Select(x => (int?)x.Id).FirstOrDefault();

            return View(users);
        }

        public async Task<IActionResult> Edit(int? id)
        {

            TempData["Users"] = "active";
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            ViewData["ConsultantId"] = new SelectList(_context.Consultant, "Id", "Name", users.ConsultantId);
            ViewData["DepartmentId"] = GetDepartmentSelectList(users.DepartmentId);
            ViewData["PersonelTitleId"] = new SelectList(_context.PersonelTitle, "Id", "Title", users.PersonelTitleId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", users.RoleId);
            ViewData["UserTypeId"] = new SelectList(_context.UserType.Where(x => x.IsActive).OrderBy(x => x.SortOrder), "Id", "Name", users.UserTypeId);
            ViewData["PersonnelUserTypeId"] = _context.UserType.Where(x => x.Code == "PERSONEL").Select(x => (int?)x.Id).FirstOrDefault();
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoleId,PersonelTitleId,DepartmentId,ConsultantId,UserTypeId,IsActive,Name,Surname,Email,Password,Country,City,Avatar,TobbUyelikOid")] Users users)
        {
            TempData["Users"] = "active";
            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Id))
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
            ViewData["ConsultantId"] = new SelectList(_context.Consultant, "Id", "Name", users.ConsultantId);
            ViewData["DepartmentId"] = GetDepartmentSelectList(users.DepartmentId);
            ViewData["PersonelTitleId"] = new SelectList(_context.PersonelTitle, "Id", "Title", users.PersonelTitleId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", users.RoleId);
            ViewData["UserTypeId"] = new SelectList(_context.UserType.Where(x => x.IsActive).OrderBy(x => x.SortOrder), "Id", "Name", users.UserTypeId);
            ViewData["PersonnelUserTypeId"] = _context.UserType.Where(x => x.Code == "PERSONEL").Select(x => (int?)x.Id).FirstOrDefault();
            return View(users);
        }

        // GET: Users/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {

            TempData["Users"] = "active";
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.Consultant)
                .Include(u => u.Department)
                .Include(u => u.PersonelTitle)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            TempData["Users"] = "active";
            if (_context.Users == null)
            {
                return Problem("Entity set 'AuthDbContext.Users'  is null.");
            }
            var users = await _context.Users.FindAsync(id);
            if (users != null)
            {
                _context.Users.Remove(users);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private SelectList GetDepartmentSelectList(int? selectedDepartmentId = null)
        {
            var departments = _context.Department
                .AsNoTracking()
                .OrderBy(x => x.Organization)
                .ThenBy(x => x.Department1)
                .ToList()
                .Select(x => new
                {
                    x.Id,
                    Name = $"{x.Organization} / {x.Department1}"
                });

            return new SelectList(departments, "Id", "Name", selectedDepartmentId);
        }
    }
}

using ADASOIdentityServer.AuthServer.Models;
using ADASOIdentityServer.Database.Contexts;
using ADASOIdentityServer.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADASOIdentityServer.AuthServer.Repository
{
    public class CustomUserRepository : ICustomUserRepository
    {
        private readonly AuthDbContext _context;

        public CustomUserRepository(AuthDbContext context)
        {
            _context = context;
        }
        public async Task<CustomUser> FindByEmail(string email)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.UserProjects)
                    .ThenInclude(up => up.Project)
                    .ThenInclude(up => up.UserProjects)
                    .ThenInclude(upr => upr.UserProjectRole)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null) return null;

            return new CustomUser()
            {
                Id = user.Id,
                OId = user.TobbUyelikOid,
                City = user.City,
                Email = user.Email,
                Password = user.Password,
                EmailConfirmed = user.EmailConfirmed,
                EmailConfirmationCode = user.EmailConfirmationCode,
                UserName = user.Name + " " + user.Surname,
                Role = user.Role?.Name,
                UserProjects = user.UserProjects.ToList()
            };
        }

        public async Task<CustomUser> FindById(int id)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Consultant)
                .Include(u => u.Department)
                .Include(u => u.PersonelTitle)
                .Include(u => u.UserProjects)
                    .ThenInclude(up => up.Project)
                .Include(u => u.UserProjects)
                    .ThenInclude(up => up.UserProjectRole)
                        .ThenInclude(upr => upr.ProjectRole)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return null;

            // UserProjects ve UserProjectRole verilerini DTO’ya map ediyoruz
            var customUser = new CustomUser
            {
                Id = user.Id,
                OId = user.TobbUyelikOid,
                City = user.City,
                Email = user.Email,
                Password = user.Password,
                EmailConfirmed = user.EmailConfirmed,
                EmailConfirmationCode = user.EmailConfirmationCode,
                EmailConfirmationExpiry = user.EmailConfirmationExpiry,
                UserName = user.Name + " " + user.Surname,
                Role = user.Role.Name,
                UserProjects = user.UserProjects.Select(up => new UserProjects
                {
                    Id = up.Id,
                    ProjectId = up.ProjectId,
                    Project = new Projects
                    {
                        Id = up.Project.Id,
                        Name = up.Project.Name
                    },
                    UserProjectRole = up.UserProjectRole.Select(upr => new UserProjectRole
                    {
                        Id = upr.Id,
                        ProjectRoleId = upr.ProjectRoleId,
                        ProjectRole = new ProjectRole
                        {
                            Id = upr.ProjectRole.Id,
                            Name = upr.ProjectRole.Name,
                            ShortName = upr.ProjectRole.ShortName
                        }
                    }).ToList()
                }).ToList()
            };

            return customUser;
        }


        public async Task<bool> Validate(string email, string password)
        {
            return await _context.Users.AnyAsync(x => x.Email == email && x.Password == password);
        }
        public async Task<List<SystemApiResources>> GetListSystemApiResourceAsync()
        {
            return await _context.SystemApiResources.ToListAsync();
        }

        public async Task<List<SystemApiScopes>> GetListSystemApiScopesAsync()
        {
            return await _context.SystemApiScopes.ToListAsync();
        }

        public async Task<List<SystemClientAllowedScopes>> GetListSystemClientAllowedScopesAsync()
        {
            return await _context.SystemClientAllowedScopes.ToListAsync();
        }

        public async Task<List<SystemClients>> GetListSystemClientsAsync()
        {
            return await _context.SystemClients.Include(x => x.SystemClientAllowedScopes).ToListAsync();
        }

        public async Task<List<SystemIdentityRosources>> GetListSystemIdentityRosourcesAsync()
        {
            return await _context.SystemIdentityRosources.ToListAsync();
        }

        public async Task<List<SystemApis>> GetListSystemApisAsync()
        {
            return await _context.SystemApis.Include(x => x.SystemApiResources).ToListAsync();
        }

        public async Task<bool> ExistUser(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }

        //AddUser
        public async Task<CustomUser> AddUser(CustomUser user)
        {
            var newUser = new Users()
            {
                Name = user.UserName.Split(" ")[0],
                Surname = user.UserName.Split(" ").Length > 1 ? user.UserName.Split(" ")[1] : "",
                Email = user.Email,
                Password = user.Password,
                EmailConfirmed = user.EmailConfirmed ?? false,
                EmailConfirmationCode = user.EmailConfirmationCode,
                EmailConfirmationExpiry = user.EmailConfirmationExpiry,
                RoleId = 8, //Default Role User
                PersonelTitleId = 1, //Default Personel Title
                DepartmentId = 1, //Default Department
                ConsultantId = 1, //Default Consultant
                Country = "Türkiye", //Default Country
                City = "Adana", //Default City
                Avatar = "default.png" //Default Avatar
            };

            _context.Users.Add(newUser);

            await _context.SaveChangesAsync();

            user.Id = newUser.Id;
            user.EmailConfirmationExpiry = newUser.EmailConfirmationExpiry;
            user.EmailConfirmationCode = newUser.EmailConfirmationCode;
            user.EmailConfirmed = newUser.EmailConfirmed;

            return user;
        }

        //UpdateUser
        public async Task<CustomUser> UpdateUser(CustomUser user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null)
            {
                return null;
            }
            existingUser.Name = user.UserName.Split(" ")[0];
            existingUser.Surname = user.UserName.Split(" ").Length > 1 ? user.UserName.Split(" ")[1] : "";
            existingUser.Email = user.Email;
            existingUser.EmailConfirmed = user.EmailConfirmed ?? existingUser.EmailConfirmed;
            existingUser.EmailConfirmationCode = user.EmailConfirmationCode ?? existingUser.EmailConfirmationCode;
            existingUser.EmailConfirmationExpiry = user.EmailConfirmationExpiry ?? existingUser.EmailConfirmationExpiry;

            if (!string.IsNullOrEmpty(user.Password))
            {
                existingUser.Password = user.Password;
            }
            existingUser.City = user.City;
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
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
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user != null)
            {
                return new CustomUser()
                {
                    Id = user.Id,
                    Email = email,
                    Password = user.Password,
                    UserName = user.Name + " " + user.Surname,
                    EmailConfirmationCode = user.EmailConfirmationCode,
                    EmailConfirmationExpiry = user.EmailConfirmationExpiry,
                    EmailConfirmed = user.EmailConfirmed

                };
            }
            else
            {
                return null;
            }
        }

        public async Task<CustomUser> FindById(int id)
        {
            var user = await _context.Users
                .Include(x => x.Role)
                .Include(x => x.UserProjects)                
                .ThenInclude(x => x.Project)                
                .Where(x => x.Id == id).SingleOrDefaultAsync();



            //###################### Planlama ##############
            //UserProject ve  UserprojectRole tablosundan join yaparak role bilgilerini de alıyoruz
            //Daha sonra user içerisinde UserProjectRoles propertysini dolduruyoruz

            //var userProjectRoles = await _context.UserProjectRole
            //    .Include(x => x.ProjectRole)
            //    .Where(x => x.UserProjects.UserId == id).ToListAsync();

            //var userProjectRoles = await _context.UserProjectRole
            //        .Where(x => x.UserProjectsId == id && x.ProjectRoleId).ToListAsync();



            if (user == null)
            {
                return null;
            }

            return new CustomUser()
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
                Projects = user.UserProjects.Select(x => x.Project).ToList(),
                UserProjectRoles = userProjectRoles

            };
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
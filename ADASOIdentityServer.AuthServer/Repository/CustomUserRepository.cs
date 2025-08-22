using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ADASOIdentityServer.AuthServer.Models;
using ADASOIdentityServer.Database.Contexts;
using ADASOIdentityServer.Database.Models;
using static Duende.IdentityServer.Models.IdentityResources;

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
                    City = user.City,
                    Email = email,
                    Password = user.Password,
                    UserName = user.Name + " " + user.Surname
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<CustomUser> FindById(int id)
        {
            var user = await _context.Users.Include(x => x.Role).Include(x=>x.UserProjects).ThenInclude(x=>x.Project).Where(x => x.Id == id).SingleOrDefaultAsync();
            
            return new CustomUser()
            {
                Id = user.Id,
                OId = user.TobbUyelikOid,
                City = user.City,
                Email = user.Email,
                Password = user.Password,
                UserName = user.Name + " " + user.Surname,
                Role = user.Role.Name,
                Projects=user.UserProjects.Select(x=>x.Project).ToList()

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

        //AddUser
        public async Task<CustomUser> AddUser(CustomUser user)
        {
            var newUser = new Users()
            {
                Name = user.UserName.Split(" ")[0],
                Surname = user.UserName.Split(" ").Length > 1 ? user.UserName.Split(" ")[1] : "",
                Email = user.Email,
                Password = user.Password,
                City = user.City,
                TobbUyelikOid = user.OId,
                //RoleId = user.RoleId
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            user.Id = newUser.Id;
            return user;
        }

    }
}
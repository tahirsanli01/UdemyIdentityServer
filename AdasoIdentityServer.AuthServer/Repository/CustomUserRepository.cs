
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AdasoIdentityServer.Database.Contexts;
using AdasoIdentityServer.Database.Models;

namespace AdasoIdentityServer.AuthServer.Repository
{
    public class CustomUserRepository : ICustomUserRepository
    {
        private readonly AuthDbContext _context;

        public CustomUserRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<Users> FindByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Users> FindById(int id)
        {
            return await _context.Users.Include(x=>x.Role).SingleOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<bool> Validate(string email, string password)
        {
            return await _context.Users.AnyAsync(x => x.Email == email && x.Password == password);
        }
    }
}

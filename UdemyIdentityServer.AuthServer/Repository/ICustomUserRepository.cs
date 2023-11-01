using System.Threading.Tasks;
using UdemyIdentityServer.Database.Models;

namespace UdemyIdentityServer.AuthServer.Repository
{
    public interface ICustomUserRepository
    {
        Task<bool> Validate(string email, string password);

        Task<Users> FindById(int id);

        Task<Users> FindByEmail(string email);
    }
}

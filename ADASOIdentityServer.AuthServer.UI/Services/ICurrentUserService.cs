using ADASOIdentityServer.AuthServer.UI.Models.Users;

namespace ADASOIdentityServer.AuthServer.UI.Services
{
    public interface ICurrentUserService
    {
        Task<UserViewModel> GetCurrentUser();
    }
}

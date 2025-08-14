using UdemyIdentityServer.AuthServer.UI.Models.Users;

namespace UdemyIdentityServer.AuthServer.UI.Services
{
    public interface ICurrentUserService
    {
        Task<UserViewModel> GetCurrentUser();
    }
}

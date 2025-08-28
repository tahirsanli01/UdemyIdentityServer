using ADASOIdentityServer.AuthServer.Models;
using System.Threading.Tasks;

namespace ADASOIdentityServer.AuthServer.Services
{
    public interface IEmailService
    {
        public Task<ServiceResult<EmailDto>> SendEmailAsync(EmailDto model);
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyIdentityServer.AuthServer.Models;
using UdemyIdentityServer.Database.Models;

namespace UdemyIdentityServer.AuthServer.Repository
{
    public interface ICustomUserRepository
    {
        Task<bool> Validate(string email, string password);

        Task<CustomUser> FindById(int id);

        Task<CustomUser> FindByEmail(string email);

        Task<List<SystemApis>> GetListSystemApisAsync();

        Task<List<SystemApiResources>> GetListSystemApiResourceAsync();

        Task<List<SystemApiScopes>> GetListSystemApiScopesAsync();

        Task<List<SystemClientAllowedScopes>> GetListSystemClientAllowedScopesAsync();

        Task<List<SystemClients>> GetListSystemClientsAsync();

        Task<List<SystemIdentityRosources>> GetListSystemIdentityRosourcesAsync();
    }
}
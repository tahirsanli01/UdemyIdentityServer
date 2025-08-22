using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ADASOIdentityServer.AuthServer.Repository;

namespace ADASOIdentityServer.AuthServer.Services
{
    public class CustomProfileService : IProfileService, IClientStore, IResourceStore
    {
        private readonly ICustomUserRepository _customUserRepository;

        public CustomProfileService(ICustomUserRepository customUserRepository)
        {
            _customUserRepository = customUserRepository;
        }

        #region FromIProfileService
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subId = context.Subject.GetSubjectId();

            var user = await _customUserRepository.FindById(int.Parse(subId));

            var claims = new List<Claim>()
            {
               new Claim(JwtRegisteredClaimNames.Email, user.Email),
               new Claim("oid", user.OId),
               new Claim("name", user.UserName),
               new Claim("city", user.City),
               new Claim("email", user.Email),
               //new Claim("project", string.Join(',', user.Projects.Select(x=>x.ShortName).ToList())),
               new Claim("role", user.Role)

            };

            foreach (var item in user.Projects)
            {
                claims.Add(new Claim("project", item.Name));
            }

            context.AddRequestedClaims(claims);
            // jwt içinde görmek istiyorsanız aşağıdaki property'i set et
            //   context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var userId = context.Subject.GetSubjectId();

            var user = await _customUserRepository.FindById(int.Parse(userId));

            context.IsActive = user != null ? true : false;
        }
        #endregion

        #region FromIClientStore
        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var data = await _customUserRepository.GetListSystemClientsAsync();

            return data.Where(x => x.ClientId == clientId).Select(x => new Client()
            {
                ClientId = x.ClientId,
                RequirePkce = x.RequirePkce ?? false,
                ClientName = x.ClientName,
                ClientSecrets = new[] { new Secret(x.ClientSecrets.Sha256()) },
                AllowedGrantTypes = new Collection<string>() { x.AllowedGrantTypes },
                RedirectUris = new List<string> { x.RedirectUris },
                PostLogoutRedirectUris = new List<string> { x.PostLogoutRedirectUris },
                AllowedScopes = x.SystemClientAllowedScopes.Select(x => x.Name).ToList(),
                AccessTokenLifetime = x.AccessTokenLifetime ?? 0,
                AllowOfflineAccess = x.AllowOfflineAccess ?? false,
                RefreshTokenUsage = (TokenUsage)(x.RefreshTokenUsage ?? 0),
                RefreshTokenExpiration = (TokenExpiration)(x.RefreshTokenExpiration ?? 0),
                AbsoluteRefreshTokenLifetime = x.AbsoluteRefreshTokenLifetime ?? 0,
                RequireConsent = x.RequireConsent ?? false
            }).SingleOrDefault();
        }
        #endregion

        #region FromIResourceStore
        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var data = await GetListIdentityRosources();
            var data2 = data.Where(x => scopeNames.Contains(x.Name)).ToList();
            return data2;
        }

        public async Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            var data = await GetListApiScopes();
            return data.Where(x => scopeNames.Contains(x.Name)).ToList();
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var data = await GetListApiResources();
            return data.Where(x => x.Scopes.Any(y => scopeNames.Contains(y))).ToList();
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            var data = await GetListApiResources();
            return data.Where(x => apiResourceNames.Contains(x.Name)).ToList();
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            return new Resources()
            {
                IdentityResources = await GetListIdentityRosources(),
                ApiResources = await GetListApiResources(),
                ApiScopes = await GetListApiScopes(),
            };
        }
        #endregion


        private async Task<List<IdentityResource>> GetListIdentityRosources()
        {
            var identityRosources = await _customUserRepository.GetListSystemIdentityRosourcesAsync();
            var paramIdentityRosources = identityRosources.Select(x =>

            new IdentityResource()
            {
                Name = x.Name,
                DisplayName = x.DisplayName,
                Description = x.Explanation,
                UserClaims = x.UserClaims.Split(',')
            }).ToList();

            if (paramIdentityRosources == null)
            {
                paramIdentityRosources = new List<IdentityResource>();
            }
            paramIdentityRosources.Add(new IdentityResources.OpenId());
            paramIdentityRosources.Add(new IdentityResources.Profile());
            return paramIdentityRosources;
        }


        private async Task<List<ApiResource>> GetListApiResources()
        {
            var apiResources = await _customUserRepository.GetListSystemApisAsync();
            var paramApiResources = apiResources.Select(x => new ApiResource(x.SystemName)
            {
                Scopes = x.SystemApiResources.Select(x => x.ApiResource + "." + x.ApiFunction).ToList(),
                ApiSecrets = new[] { new Secret(x.ApiSecrets.Sha256()) }
            }).ToList();
            return paramApiResources;
        }

        private async Task<List<ApiScope>> GetListApiScopes()
        {
            var apiScopes = await _customUserRepository.GetListSystemApiScopesAsync();
            var paramApiScopes = apiScopes.Select(x => new ApiScope(x.ApiResource + "." + x.Scope, x.Explanation)).ToList();
            return paramApiScopes;
        }
    }
}
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using System;
using System.Collections.Generic;

namespace ADASOIdentityServer.AuthServer
{
    public static class Config
    {
        //Api kaynakları belirlenir,
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("resource_api1")
                {
                    Scopes={ "api1.read","api1.write","api1.update" }
                    ,ApiSecrets = new []{new  Secret("secretapi1".Sha256())}
                },
                new ApiResource("resource_api2")
                {

                    Scopes={ "api2.read","api2.write","api2.update" }
                    ,ApiSecrets = new []{new  Secret("secretapi2".Sha256()) }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>()
            {
                new ApiScope("api1.read","API 1 için okuma izni")
                ,new ApiScope("api1.write","API 1 için yazma izni")
                ,new ApiScope("api1.update","API 1 için güncelleme izni")
                ,new ApiScope("api2.read","API 2 için okuma izni")
                ,new ApiScope("api2.write","API 2 için yazma izni")
                ,new ApiScope("api2.update","API 2 için güncelleme izni"),
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(), //subId
                new IdentityResources.Profile(), ///
                new IdentityResource(){ Name="Projects", DisplayName="Proje Bilgisi",Description="Proje bilgisi", UserClaims= new [] {"project"}},
                new IdentityResource(){ Name="CountryAndCity", DisplayName="Country and City",Description="Kullanıcının ülke ve şehir bilgisi", UserClaims= new [] {"country","city"}},
                new IdentityResource(){ Name="Roles",DisplayName="Roles", Description="Kullanıcı rolleri", UserClaims=new [] { "role"} }
            };
        }

        public static IEnumerable<Client> GetClients() // +++
        {
            return new List<Client>()
            {
                 new Client()
                 {
                    ClientId = "Advisor-MVC-Project"
                    ,RequirePkce=false
                    ,ClientName="Advisor-MVC-Project app  mvc uygulaması"
                    ,ClientSecrets=new[] {new Secret("secret".Sha256())}
                    ,AllowedGrantTypes= GrantTypes.Hybrid
                    ,RedirectUris=new  List<string>{ "https://localhost:5005/signin-oidc" }
                    ,PostLogoutRedirectUris=new List<string>{ "https://localhost:5005/signout-callback-oidc" }
                    ,AllowedScopes = {IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, "api1.read",IdentityServerConstants.StandardScopes.OfflineAccess,"CountryAndCity","Roles", "Projects" }
                    ,AccessTokenLifetime=2*60*60
                    ,AllowOfflineAccess=true
                    ,RefreshTokenUsage=TokenUsage.ReUse
                    ,RefreshTokenExpiration=TokenExpiration.Absolute
                    ,AbsoluteRefreshTokenLifetime=(int) (DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds
                    ,RequireConsent=false
                 },

                 new Client()
                 {
                    ClientId = "Client2-Mvc"
                    ,RequirePkce=false
                    ,ClientName="Client 2 app  mvc uygulaması"
                    ,ClientSecrets=new[] {new Secret("secret".Sha256())}
                    ,AllowedGrantTypes= GrantTypes.Hybrid
                    ,RedirectUris=new  List<string>{ "https://localhost:5005/signin-oidc" }
                    ,PostLogoutRedirectUris=new List<string>{ "https://localhost:5005/signout-callback-oidc" }
                    ,AllowedScopes = {IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, "api1.read",IdentityServerConstants.StandardScopes.OfflineAccess,"CountryAndCity","Roles"}
                    ,AccessTokenLifetime=2*60*60
                    ,AllowOfflineAccess=true
                    ,RefreshTokenUsage=TokenUsage.ReUse
                    ,RefreshTokenExpiration=TokenExpiration.Absolute
                    ,AbsoluteRefreshTokenLifetime=(int) (DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds
                    ,RequireConsent=false
                 },
            };
        }
    }
}
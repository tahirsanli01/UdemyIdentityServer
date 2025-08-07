using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdasoAdvisor.Helper;

    public class AuthHelper
{
    public IHttpContextAccessor _httpContextAccessor { get; set; }
    public AuthHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int GetUserId()
    {
        var data = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
        if (data != null)
        {
            return Convert.ToInt32(data.Value);
        }
        else
        {
            throw new UnauthorizedAccessException();
        }
    }

    public string GetUserOId()
    {
        var data = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(x => x.Type == "oid");
        if (data != null)
        {
            return data.Value;
        }
        else
        {
            throw new UnauthorizedAccessException();
        }
    }

    public string GetUserName()
    {
        var data = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(x => x.Type == "name");
        if (data != null)
        {
            return data.Value;
        }
        else
        {
            throw new UnauthorizedAccessException();
        }
    }


    public string GetUserCity()
    {
        var data = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(x => x.Type == "city");
        if (data != null)
        {
            return data.Value;
        }
        else
        {
            throw new UnauthorizedAccessException();
        }
    }


    public string GetUserRole()
    {
        var data = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(x => x.Type == "role");
        if (data != null)
        {
            return data.Value;
        }
        else
        {
            throw new UnauthorizedAccessException();
        }
    }

    public List<string> GetUserProjects()
    {
        var data = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(x => x.Type == "project");
        if (data != null)
        {

            return data.Value.Split(',').ToList();
        }
        else
        {
            throw new UnauthorizedAccessException();
        }
    }

    //[HttpPost]
    //public async Task GetLogoutRequest()
    //{
    //    await _httpContextAccessor.HttpContext.SignOutAsync("Cookies");
    //    await _httpContextAccessor.HttpContext.SignOutAsync("oidc");
    //    _httpContextAccessor.HttpContext.Session.Clear();
    //    await _httpContextAccessor.HttpContext.Session.CommitAsync();        
    //}
}

using ADASOIdentityServer.AuthServer.UI.Models.ComponentViewDtos;
using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class CommentsParitial : ViewComponent
    {
        public IViewComponentResult Invoke(TodoCardDto model)
        {
            return View("CommentsParitial", model);
        }
    }
}
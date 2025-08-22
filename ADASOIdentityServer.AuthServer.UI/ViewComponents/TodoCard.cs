using ADASOIdentityServer.AuthServer.UI.Models.ComponentViewDtos;
using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class TodoCard : ViewComponent
    {
        public IViewComponentResult Invoke(TodoCardDto model)
        {
            return View("TodoCard", model);
        }
    }
}
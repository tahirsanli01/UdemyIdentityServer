using ADASOIdentityServer.AuthServer.UI.Models.ComponentViewDtos;
using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
{
    public class MenuForTodoCard : ViewComponent
    {
        public IViewComponentResult Invoke(MenuForTodoCardDto model)
        {
            return View("MenuForTodoCard", model);
        }
    }
}
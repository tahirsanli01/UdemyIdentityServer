using AdasoAdvisor.Models.ComponentViewDtos;
using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class MenuForTodoCard : ViewComponent
    {
        public IViewComponentResult Invoke(MenuForTodoCardDto model)
        {
            return View("MenuForTodoCard", model);
        }
    }
}
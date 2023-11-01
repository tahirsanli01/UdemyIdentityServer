using AdasoAdvisor.Models.ComponentViewDtos;
using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class TodoCard : ViewComponent
    {
        public IViewComponentResult Invoke(TodoCardDto model)
        {
            return View("TodoCard", model);
        }
    }
}
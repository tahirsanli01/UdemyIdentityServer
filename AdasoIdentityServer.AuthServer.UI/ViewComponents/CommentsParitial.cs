using AdasoAdvisor.Models.ComponentViewDtos;
using Microsoft.AspNetCore.Mvc;

namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class CommentsParitial : ViewComponent
    {
        public IViewComponentResult Invoke(TodoCardDto model)
        {
            return View("CommentsParitial", model);
        }
    }
}
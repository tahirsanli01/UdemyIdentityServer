using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class HeadMenu : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            
            return View("HeadMenu");
        }
    }
}
using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class DrawerAttachmentProcess : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View("DrawerAttachmentProcess");
        }
    }
}
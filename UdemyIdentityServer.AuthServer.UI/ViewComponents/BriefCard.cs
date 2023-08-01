using AdasoAdvisor.Helper;
using AdasoAdvisor.Models.ComponentViewDtos;
using Microsoft.AspNetCore.Mvc;

namespace AdasoAdvisor.Controllers.ViewComponents
{
	public class BriefCard : ViewComponent
	{

		public IViewComponentResult Invoke(BriefCardDto briefCardDto)
		{
            //return View("BriefCard", briefCardDto);
			var data = KPIHelper.Get();
            return View("BriefCard", new BriefCardDto(data.Title, "", data.ActivityCount,100,CardBgColors.GetRandomColor()));
        }
	}
}
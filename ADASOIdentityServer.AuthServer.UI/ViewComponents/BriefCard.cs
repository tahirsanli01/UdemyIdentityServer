using ADASOIdentityServer.AuthServer.UI.Helper;
using ADASOIdentityServer.AuthServer.UI.Models.ComponentViewDtos;
using Microsoft.AspNetCore.Mvc;

namespace ADASOIdentityServer.AuthServer.UI.ViewComponents
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
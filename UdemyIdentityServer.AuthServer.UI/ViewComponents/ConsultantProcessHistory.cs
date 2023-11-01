using AdasoAdvisor.Models.ComponentViewDtos;
using Microsoft.AspNetCore.Mvc;

namespace AdasoAdvisor.Controllers.ViewComponents
{
	public class ConsultantProcessHistory : ViewComponent
	{

		public IViewComponentResult Invoke(List<ConsultantProcessHistoryDto> consultantProcessHistoryDtos)
        {
            Random rnd = new Random();
    
            for (int i = 0; i < 10; i++)
			{
                var count = rnd.Next() % 5;
                consultantProcessHistoryDtos.Add(new ConsultantProcessHistoryDto("İsmail Gündoğan", "Outlines keep you honest.They stop you from indulging in poorly thought-out metaphors about driving and keep you focused on the overall structure of your post", "assets/media/avatars/blank.png", count, count, DateTime.Now));
			}
			return View("ConsultantProcessHistory", consultantProcessHistoryDtos);
		}
	}
}
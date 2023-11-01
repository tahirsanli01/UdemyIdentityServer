using AdasoAdvisor.Models.ComponentViewDtos;
using Microsoft.AspNetCore.Mvc;

  namespace AdasoAdvisor.Controllers.ViewComponents
{
    public class Timeline : ViewComponent
    {

        public IViewComponentResult Invoke(List<TimelineDto.TaskForWeek> tasks)
        {
            var result = new TimelineDto("Etkinlik Takvimi", tasks);
           
            foreach (var item in result.DayOfWeeks)
            {
                Random rnd = new Random();
                var count = rnd.Next()%4;

                for (int i = 0; i < count; i++)
                {
                    tasks.Add(new TimelineDto.TaskForWeek(item.CurrentDate, item.CurrentDate.AddHours(-i),"Test Title", "İsmail Gündoğan"));
				}
            }

			return View("Timeline", result);
        }
    }
}
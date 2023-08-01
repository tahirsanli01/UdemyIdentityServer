using AdasoAdvisor.Models.ComponentViewDtos;
using Microsoft.AspNetCore.Mvc;

namespace AdasoAdvisor.Controllers.ViewComponents
{
	public class LatestFiles : ViewComponent
	{

		public IViewComponentResult Invoke(LatestFilesDto latestFileDto)
		{

			latestFileDto.LatestFiles = new List<LatestFileItem>();

			Random rnd = new Random();
			var count = rnd.Next() % 5;
			for (int i = 0; i < count; i++)
			{
				latestFileDto.LatestFiles.Add(new LatestFileItem($"Test Title{i}", GetRandomFilePath(), "İsmail Gündoğan",DateTime.Now.AddDays(count)));
			}

			return View("LatestFiles", latestFileDto);
		}

		public string GetRandomFilePath()
		{
            Random rnd = new Random();
            var count = rnd.Next() % 5;

			switch (count)
			{
                case 0: return $"xyz.pdf";
                case 1: return $"xyz.doc";
                case 2: return $"xyz.css";
                case 3: return $"xyz.xls";
                case 4: return $"xyz.png";
                case 5: return $"xyz.jpg";
                
                default:
                    return "";
            }
		}
	}
}
namespace AdasoAdvisor.Models.ComponentViewDtos
{
	public class LatestFilesDto
	{
		public LatestFilesDto(string title, List<LatestFileItem> latestFiles)
		{
			Title = title;
			LatestFiles = latestFiles;
			TotalCount = latestFiles.Count();
		}

		public string Title { get; set; }
		public List<LatestFileItem> LatestFiles { get; set; }

		public int TotalCount { get; set; }


	}

	public class LatestFileItem
	{
		public LatestFileItem(string title, string iconPath, string personFullName, DateTime insertDate)
		{
			Title = title;
			IconPath = iconPath;
			PersonFullName = personFullName;
			InsertDate = insertDate;
		}

		public string Title { get; set; }
        public string IconPath { get; set; }
        public string PersonFullName { get; set; }
        public DateTime InsertDate { get; set; }

		public string Icon
		{
			get { return $"assets/media/svg/files/{Path.GetExtension(IconPath).Replace(".","")}.svg"; }
		}


		public string InsertDateString
		{
			get { return ((int)(DateTime.Now - InsertDate).TotalMinutes).ToString() + " önce eklendi"; }
		}

	}
}

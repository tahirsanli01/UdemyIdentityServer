namespace AdasoAdvisor.Models.ComponentViewDtos
{
    public class BriefCardDto
    {
        public BriefCardDto(string title, string activityTypeName, int activityCount, double completedPercent, string bgColor)
        {
            Title = title;
            ActivityTypeName = activityTypeName;
            ActivityCount = activityCount;
            CompletedPercent = completedPercent;
            BgColor = bgColor;
        }

        public string Title { get; set; }
        public string ActivityTypeName { get; set; }
        public int ActivityCount { get; set; }
        public double CompletedPercent { get; set; }
        public string BgColor { get; set; }
    }
}

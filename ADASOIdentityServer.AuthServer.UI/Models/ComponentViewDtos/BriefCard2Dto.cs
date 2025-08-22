namespace ADASOIdentityServer.AuthServer.UI.Models.ComponentViewDtos
{
    public class BriefCard2Dto
    {
		public BriefCard2Dto(string title, int activityCount, string bgColor)
		{
			Title = title;
			ActivityCount = activityCount;
			BgColor = bgColor;
		}

		public string Title { get; set; }
        public int ActivityCount { get; set; }
        public string BgColor { get; set; }

    }


    public static class CardBgColors
    {
        public static string Warning { get; set; } = "warning";
        public static string Success { get; set; } = "success";
        public static string Primary { get; set; } = "primary";
        public static string Dark { get; set; } = "dark";
        public static string Danger { get; set; } = "danger";

        public static string GetRandomColor()
        {
            Random rnd = new Random();
            switch (rnd.Next()%5)
            {
                case 0: return Warning;
                case 1: return Success;
                case 2: return Primary;
                case 3: return Dark;
                case 4: return Danger;
                default: return "";
            }
        }
    }
}

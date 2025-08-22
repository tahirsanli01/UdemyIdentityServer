namespace ADASOIdentityServer.AuthServer.UI.Models.ComponentViewDtos
{
	public class TodoCardDto
	{
        public TodoCardDto(string title, string subTitle, string explanation, int messageCount, int attachmentCount, string companyName, TodoCardType cardType)
        {
            Title = title;
            SubTitle = subTitle;
            Explanation = explanation;
            MessageCount = messageCount;
            AttachmentCount = attachmentCount;
            CompanyName = companyName;
            CardType = cardType;
        }

        public string Title { get; set; }
		public string SubTitle { get; set; }
		public string Explanation { get; set; }
		public int MessageCount { get; set; }
		public int AttachmentCount { get; set; }
		public string CompanyName { get; set; }
        public TodoCardType CardType { get; set; }

    }
}

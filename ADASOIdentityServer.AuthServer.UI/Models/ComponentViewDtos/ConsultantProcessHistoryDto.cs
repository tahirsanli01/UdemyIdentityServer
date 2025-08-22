using Microsoft.Extensions.Hosting;

namespace ADASOIdentityServer.AuthServer.UI.Models.ComponentViewDtos
{
	public class ConsultantProcessHistoryDto
	{
        public ConsultantProcessHistoryDto(string personFullName, string processExplanation, string avatar, int totalMessageCount, int totalAttachmentCount, DateTime processDateTime)
        {
            PersonFullName = personFullName;
            ProcessExplanation = processExplanation;
            Avatar = avatar;
            TotalMessageCount = totalMessageCount;
            TotalAttachmentCount = totalAttachmentCount;
            ProcessDateTime = processDateTime;
        }

        public string PersonFullName { get; set; }

        public string ProcessExplanation { get; set; }

        public string Avatar { get; set; }

        public int TotalMessageCount { get; set; }
        public int TotalAttachmentCount { get; set; }

        public DateTime ProcessDateTime { get; set;}

		public string ProcessDateTimeString
        {
			get { return ProcessDateTime.ToString("dd/MM/yyyy mm:hh"); }
		}


	}
}

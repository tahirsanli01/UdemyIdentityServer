using static ADASOIdentityServer.AuthServer.UI.Models.ComponentViewDtos.TimelineDto.OneWeekDay;

namespace ADASOIdentityServer.AuthServer.UI.Models.ComponentViewDtos
{
	public class TimelineDto
	{
        public string Title { get; set; }
        public List<OneWeekDayItem> DayOfWeeks { get; set; }
		public List<TaskForWeek> TaskForWeeks { get; set; }

		public TimelineDto(string title, List<TaskForWeek> taskForWeeks)
		{
            Title = title;
            DayOfWeeks = new OneWeekDay(DateTime.Now).items;
            TaskForWeeks = taskForWeeks;
		}


		public class OneWeekDay
		{
			public List<OneWeekDayItem> items = new List<OneWeekDayItem>();
			public OneWeekDay(DateTime now)
			{
				DateTime firstDayOfWeek = DateTime.Now;
				DateTime lastDayOfWeek = DateTime.Now;
				DateTime dt_Hafta = DateTime.Now;
				switch ((int)now.DayOfWeek)
				{
					case 0://Haftanın ilk günü Pazar kabul edildiğinden
						firstDayOfWeek = dt_Hafta.AddDays(-6); // İçinde olduğumuz haftanın başı Pazartesi
						lastDayOfWeek = dt_Hafta.AddDays(1); // Sonraki haftanın başı Pazartesi
						break;

					default:// Gün Pazar değilse;
						firstDayOfWeek = dt_Hafta.AddDays(1 - (int)now.DayOfWeek); // İçinde olduğumuz haftanın başı Pazartesi
						lastDayOfWeek = firstDayOfWeek.AddDays(7); //  Sonraki haftanın başı Pazartesi
						break;
				}

				for (; firstDayOfWeek < lastDayOfWeek; firstDayOfWeek = firstDayOfWeek.AddDays(1))
				{
					items.Add(new OneWeekDayItem(firstDayOfWeek, firstDayOfWeek.Day, GetDayOfWeekName(firstDayOfWeek.DayOfWeek), GetDayOfWeekShortName(firstDayOfWeek.DayOfWeek)));
				}
			}

			private string GetDayOfWeekName(DayOfWeek dayOfWeek)
			{
				switch ((int)dayOfWeek)
				{
					case 0: return "Pazar";
					case 1: return "Pazartesi";
					case 2: return "Salı";
					case 3: return "Çarşamba";
					case 4: return "Perşembe";
					case 5: return "Cuma";
					case 6: return "Cumartesi";

					default: return "";
				}
			}

			private string GetDayOfWeekShortName(DayOfWeek dayOfWeek)
			{
				switch ((int)dayOfWeek)
				{
					case 0: return "Paz";
					case 1: return "Pzt";
					case 2: return "Sa";
					case 3: return "Çar";
					case 4: return "Per";
					case 5: return "Cum";
					case 6: return "Cmts";

					default: return "";
				}
			}

			public class OneWeekDayItem
			{
				public OneWeekDayItem(DateTime currentDate, int dayOfWeek, string dayOfWeekString, string dayOfWeekShortString)
				{
					CurrentDate = currentDate;
					DayOfWeek = dayOfWeek;
					DayOfWeekString = dayOfWeekString;
					DayOfWeekShortString = dayOfWeekShortString;
				}

				public DateTime CurrentDate { get; set; }
				public int DayOfWeek { get; set; }
				public string DayOfWeekString { get; set; }
				public string DayOfWeekShortString { get; set; }
			}
		}

		public class TaskForWeek
		{
			public TaskForWeek(DateTime taskStartDateTime, DateTime taskEndDateTime, string title, string personFullName)
			{
				TaskStartDateTime = taskStartDateTime;
				TaskEndDateTime = taskEndDateTime;
				Title = title;
				PersonFullName = personFullName;
			}

			public DateTime TaskStartDateTime { get; set; }
			public DateTime TaskEndDateTime { get; set; }
			public string Title { get; set; }
			public string PersonFullName { get; set; }

			public string TaskStartTime
			{
				get { return TaskStartDateTime.ToString("hh:mm"); }
			}
			public string TaskEndTime
			{
				get { return TaskEndDateTime.ToString("hh:mm"); }
			}

		}
	}
}

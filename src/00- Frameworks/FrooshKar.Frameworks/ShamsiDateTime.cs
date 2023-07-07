using System.Data;
using System.Globalization;

namespace FrooshKar.Frameworks
{
	public static class ShamsiCalender
    {
        
        public static string ToPersianCalenderWithHour(this DateTime dateTime)
		{
			PersianCalendar persianCalendar = new PersianCalendar();
			var year =persianCalendar.GetYear(dateTime);
			var month = persianCalendar.GetMonth(dateTime);
			var dayOfMonth = persianCalendar.GetDayOfMonth(dateTime);
			var hour = persianCalendar.GetHour(dateTime);
			var minute = persianCalendar.GetMinute(dateTime);
			var second = persianCalendar.GetSecond(dateTime);

			return $"{year}/{month}/{dayOfMonth} {hour}:{minute}:{second}";

		}


		public static string ToPersianCalenderWithoutHour(this DateTime? dateTime)
		{
            PersianCalendar persianCalendar = new PersianCalendar();
            var year = persianCalendar.GetYear((DateTime)dateTime);
			var month = persianCalendar.GetMonth((DateTime)dateTime);
			var dayOfMonth = persianCalendar.GetDayOfMonth((DateTime)dateTime);


			return $"{year}/{month}/{dayOfMonth}";


		}


		public static DateTime ToEnglishCalender(this string persianCalender)
		{
			PersianCalendar pc = new PersianCalendar();
			var strings = persianCalender.Split('/');

			var dateTime =
				new DateTime(Convert.ToInt32(strings[0]), Convert.ToInt32(strings[1]), Convert.ToInt32(strings[2]),pc);
			return dateTime;

		}



	}
}
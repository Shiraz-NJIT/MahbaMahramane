using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahbaExtention
{
    /// <summary>
    /// تقویم شمسی
    /// </summary>
    public static class PersianCalendar
    {
        static PersianCalendar()
        {
            _Calendar = new System.Globalization.PersianCalendar();
        }

        private static System.Globalization.PersianCalendar _Calendar;
        public static System.Globalization.PersianCalendar Calendar
        {
            get
            {
                return _Calendar;
            }
            set
            {
                _Calendar = value;
            }
        }

        /// <summary>
        /// دریافت تاریخ و ساعت شمسی
        /// </summary>
        public static string GetDateTime(DateTime datetime)
        {
            return GetDate(datetime) + " " + GetTime(datetime);
        }

        /// <summary>
        /// دریافت تاریخ امروز به شمسی
        /// </summary>
        public static string GetDate()
        {
            return GetDate(DateTime.Now, "/");
        }

        /// <summary>
        /// دریافت تاریخ شمسی
        /// </summary>
        public static string GetDate(DateTime datetime)
        {
            return GetDate(datetime, "/");
        }

        /// <summary>
        /// دریافت تاریخ شمسی
        /// </summary>
        /// <param name="datetime">تاریخ و ساعت</param>
        /// <param name="separator">جدا کننده</param>
        /// <returns>تاریخ به شمسی برگردانده می شود</returns>
        public static string GetDate(DateTime datetime, string separator)
        {
            try
            {
                return string.Format("{0}{3}{1}{3}{2}", Calendar.GetYear(datetime).ToString("####0000"), Calendar.GetMonth(datetime).ToString("##00"), Calendar.GetDayOfMonth(datetime).ToString("##00"), separator);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// دریافت سال شمسی
        /// </summary>
        public static int GetYear(DateTime datetime)
        {
            return Calendar.GetYear(datetime);
        }

        /// <summary>
        /// دریافت ماه شمسی
        /// </summary>
        public static int GetMonth(DateTime datetime)
        {
            return Calendar.GetMonth(datetime);
        }

        /// <summary>
        /// دریافت روز شمسی
        /// </summary>
        public static int GetDay(DateTime datetime)
        {
            return Calendar.GetDayOfMonth(datetime);
        }

        /// <summary>
        /// دریافت ساعت به صورت 24 ساعتی
        /// </summary>
        public static string GetTime()
        {
            return GetTime(DateTime.Now, ":", false);
        }

        /// <summary>
        /// دریافت ساعت به صورت 24 ساعتی
        /// </summary>
        public static string GetTime(DateTime datetime)
        {
            return GetTime(datetime, ":", false);
        }

        /// <summary>
        /// دریافت ساعت به صورت 24 ساعتی
        /// </summary>
        /// <param name="separator">جدا کننده</param>
        public static string GetTime(string separator)
        {
            return GetTime(DateTime.Now, separator, false);
        }

        /// <summary>
        /// دریافت ساعت به صورت 24 ساعتی
        /// </summary>
        /// <param name="datetime">تاریخ و ساعت</param>
        /// <param name="separator">جدا کننده</param>
        /// <param name="withSecond">ثانیه هم منظور گردد؟</param>
        public static string GetTime(DateTime datetime, string separator, bool withSecond)
        {
            return string.Format("{0}{3}{1}{2}", datetime.Hour.ToString("##00"), datetime.Minute.ToString("##00"), withSecond ? (separator + datetime.Second.ToString("##00")) : "", separator);
        }

        public static string GetTime(DateTime datetime, string separator, bool withSecond, bool withMiliSecond)
        {
            return string.Format("{0}{4}{1}{2}{3}", datetime.Hour.ToString("##00"), datetime.Minute.ToString("##00"), withSecond ? (separator + datetime.Second.ToString("##00")) : "", withMiliSecond ? (separator + datetime.Millisecond.ToString("##00")) : "", separator);
        }

        /// <summary>
        /// مقایسه دو تاریخ و برگشت تعداد روزهای اختلاف بین این دو تاریخ
        /// </summary>
        /// <param name="date1">تاریخ شمسی</param>
        /// <param name="date2">تاریخ شمسی</param>
        /// <returns>اختلاف روزهای دو تاریخ برگشت داده می شود</returns>
        public static int CompareDate(string date1, string date2)
        {
            if (date1.CompareTo(date2) > 0)
            {
                int variance = 0;
                do
                {
                    variance--;
                    date2 = AddDays(date2, 1);
                } while (date1.CompareTo(date2) > 0);
                return variance;
            }
            else if (date1.CompareTo(date2) < 0)
            {
                int variance = 0;
                do
                {
                    variance++;
                    date1 = AddDays(date1, 1);
                } while (date1.CompareTo(date2) < 0);
                return variance;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// مقایسه دو تاریخ و برگشت تعداد ماه های اختلاف بین این دو تاریخ
        /// </summary>
        /// <param name="date1">تاریخ شمسی</param>
        /// <param name="date2">تاریخ شمسی</param>
        public static int CompareDateMonths(string date1, string date2)
        {
            if (date1.CompareTo(date2) > 0)
            {
                int variance = 0;
                do
                {
                    variance--;
                    date2 = AddOneMonth(date2);
                } while (date1.CompareTo(date2) > 0);
                return variance;
            }
            else if (date1.CompareTo(date2) < 0)
            {
                int variance = 0;
                do
                {
                    variance++;
                    date1 = AddOneMonth(date1);
                } while (date1.CompareTo(date2) < 0);
                return variance;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// افزودن روز به تاریخ
        /// </summary>
        /// <param name="date">تاریخ شمسی</param>
        /// <param name="days">مقدار روز که میتواند منفی هم باشد</param>
        public static string AddDays(string date, int days)
        {
            DateTime d = ToDateTime(date);
            d = d.AddDays(days);
            return GetDate(d);
        }

        public static string AddOneMonth(string date)
        {
            string[] arr = date.Split('/');
            int y, m, d;
            try
            {
                y = int.Parse(arr[0]);
                m = int.Parse(arr[1]);
                d = int.Parse(arr[2]);
            }
            catch
            {
                return date;
            }
            if (m == 12)
                y++;
            else
                m++;
            return y.ToString("0000") + "/" + (m).ToString("00") + "/" + d.ToString("00");
        }

        /// <summary>
        /// دریافت نام ماه
        /// </summary>
        /// <param name="month">ماه</param>
        public static string GetMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "فروردین";
                case 2:
                    return "اردیبهشت";
                case 3:
                    return "خرداد";
                case 4:
                    return "تیر";
                case 5:
                    return "مرداد";
                case 6:
                    return "شهریور";
                case 7:
                    return "مهر";
                case 8:
                    return "آبان";
                case 9:
                    return "آذر";
                case 10:
                    return "دی";
                case 11:
                    return "بهمن";
                case 12:
                    return "اسفند";
                default:
                    return "";
            }
        }

        /// <summary>
        /// دریافت نام ماه
        /// </summary>
        /// <param name="datetime">زمان</param>
        public static string GetMonthName(DateTime datetime)
        {
            return GetMonthName(Calendar.GetMonth(datetime));
        }

        /// <summary>
        /// دریافت عدد روز هفته
        /// اخرین روز هفته=7   اولین روز هفته=1 
        /// </summary>
        public static int GetDayOfWeekCode(DateTime datetime)
        {
            switch (Calendar.GetDayOfWeek(datetime))
            {
                case DayOfWeek.Friday:
                    return 7;
                case DayOfWeek.Monday:
                    return 3;
                case DayOfWeek.Saturday:
                    return 1;
                case DayOfWeek.Sunday:
                    return 2;
                case DayOfWeek.Thursday:
                    return 6;
                case DayOfWeek.Tuesday:
                    return 4;
                case DayOfWeek.Wednesday:
                    return 5;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// دریافت روز هفته
        /// </summary>
        public static string GetDayOfWeekName(DateTime datetime)
        {
            switch (Calendar.GetDayOfWeek(datetime))
            {
                case DayOfWeek.Friday:
                    return "جمعه";
                case DayOfWeek.Monday:
                    return "دو شنبه";
                case DayOfWeek.Saturday:
                    return "شنبه";
                case DayOfWeek.Sunday:
                    return "یك شنبه";
                case DayOfWeek.Thursday:
                    return "پنج شنبه";
                case DayOfWeek.Tuesday:
                    return "سه شنبه";
                case DayOfWeek.Wednesday:
                    return "چهار شنبه";
                default:
                    return "";
            }
        }

        /// <summary>
        /// دریافت تاریخ با نام ماه
        /// </summary>
        /// <param name="datetime">زمان</param>
        /// <param name="separator">جدا کننده</param>
        public static string GetDateWithMonthName(DateTime datetime, string separator)
        {
            if (string.IsNullOrEmpty(separator))
                separator = "/";
            string s;
            s = Calendar.GetDayOfMonth(datetime).ToString();
            s += separator;
            s += GetMonthName(datetime);
            s += separator;
            s += Calendar.GetYear(datetime).ToString();
            return s;
        }

        /// <summary>
        /// دریافت تاریخ با نام ماه
        /// </summary>
        public static string GetDateWithMonthName(DateTime datetime)
        {
            return GetDateWithMonthName(datetime, "/");
        }

        /// <summary>
        /// آیا تاریخ یک تاریخ درست است
        /// </summary>
        /// <param name="date">تاریخ شمسی</param>
        public static bool DateIsCorrect(string date)
        {
            return DateIsCorrect(date, '/');
        }

        /// <summary>
        /// آیا تاریخ یک تاریخ درست است
        /// </summary>
        /// <param name="date">تاریخ شمسی</param>
        /// <param name="separator">جدا کننده</param>
        public static bool DateIsCorrect(string date, char separator)
        {
            if (string.IsNullOrEmpty(date))
                return false;
            string[] arr = date.Split(separator);
            if (arr.Length != 3)
            {
                separator = '\\';
                arr = date.Split(separator);
                if (arr.Length != 3)
                    return false;
            }
            int y, m, d;
            try
            {
                y = int.Parse(arr[0]);
                m = int.Parse(arr[1]);
                d = int.Parse(arr[2]);
            }
            catch
            {
                return false;
            }
            if (!(d >= 1 && d <= 31))
                return false;
            if (!(m >= 1 && m <= 12))
                return false;
            if (!(y >= 1 && y <= 1900))
                return false;

            int days;
            try
            {
                days = GetNumberOfDays(ToDateTime(date, separator));
            }
            catch
            {
                return false;
            }
            if (!(d >= 1 && d <= days))
                return false;
            return true;
        }

        /// <summary>
        /// دریافت تعداد روزهای ماه
        /// </summary>
        /// <param name="date">زمان</param>
        public static int GetNumberOfDays(DateTime date)
        {
            return Calendar.GetDaysInMonth(GetYear(date), GetMonth(date));
        }

        /// <summary>
        /// تبدیل تاریخ شمسی
        /// </summary>
        /// <param name="date">تاریخ شمسی</param>
        /// <param name="separator">جدا کننده</param>
        public static DateTime ToDateTime(string date, char separator)
        {
            string[] s = date.Split(separator);

            if (s.Length != 3)
            {
                separator = '\\';
                s = date.Split(separator);
            }
            if (s[0].Length == 2)
                s[0] = "13" + s[0];
            int y = int.Parse(s[0]);
            int m = int.Parse(s[1]);
            int d = int.Parse(s[2]);
            return Calendar.ToDateTime(y, m, d, 12, 0, 0, 0);
        }

        /// <summary>
        /// تبدیل تاریخ شمسی
        /// </summary>
        /// <param name="date">تاریخ شمسی</param>
        /// <param name="time">ساعت</param>
        /// <param name="dateSeparator">جدا کننده تاریخ</param>
        /// <param name="timeSeparator">جدا کننده ساعت</param>
        public static DateTime ToDateTime(string date, string time, char dateSeparator, char timeSeparator)
        {
            string[] dateArr = date.Split(dateSeparator);
            int year = int.Parse(dateArr[0]);
            int month = int.Parse(dateArr[1]);
            int day = int.Parse(dateArr[2]);
            string[] timeArr = time.Split(timeSeparator);
            int hour = int.Parse(timeArr[0]);
            int min = int.Parse(timeArr[1]);
            return Calendar.ToDateTime(year, month, day, hour, min, 0, 0);
        }

        /// <summary>
        /// تبدیل تاریخ شمسی
        /// </summary>
        /// <param name="date">تاریخ شمسی</param>
        public static DateTime ToDateTime(string date)
        {
            return ToDateTime(date, '/');
        }

        /// <summary>
        /// آیا ساعت یک ساعت صحیح است
        /// </summary>
        /// <param name="time">ساعت</param>
        public static bool TimeIsCorrect(string time)
        {
            return TimeIsCorrect(time, ':');
        }

        /// <summary>
        /// آیا ساعت یک ساعت صحیح است
        /// </summary>
        /// <param name="time">ساعت</param>
        /// <param name="separator">جدا کننده</param>
        public static bool TimeIsCorrect(string time, char separator)
        {
            if (string.IsNullOrEmpty(time))
                return false;
            string[] arr = time.Split(separator);
            if (arr.Length != 2)
                return false;
            int h, m;
            try
            {
                h = int.Parse(arr[0]);
                m = int.Parse(arr[1]);
            }
            catch
            {
                return false;
            }
            if (!(h >= 0 && h <= 23))
                return false;
            if (!(m >= 0 && m <= 59))
                return false;

            return true;
        }

        public static string GetTimeReverced(DateTime datetime, string separator, bool withSecond)
        {
            return string.Format("{0}{1}{3}{2}", withSecond ? (datetime.Second.ToString("##00") + separator) : "", datetime.Minute.ToString("##00"), datetime.Hour.ToString("##00"), separator);
        }

        public static string AddMinutesToTime(int hour, int minute, int minutesToAdd)
        {
            return GetTime(new DateTime(9999, 1, 1, hour, minute, 0).AddMinutes(minutesToAdd));
        }

        public static string AddMinutesToTime(string time, int minutesToAdd)
        {
            int hour, minute;
            string[] arr = time.Split(':');
            hour = int.Parse(arr[0]);
            minute = int.Parse(arr[1]);
            return AddMinutesToTime(hour, minute, minutesToAdd);
        }

        public static string AddHourToTime(int hour, int minute, int hoursToAdd)
        {
            return GetTime(new DateTime(9999, 1, 1, hour, minute, 0).AddHours(hoursToAdd));
        }

        public static string AddHourToTime(string time, int hoursToAdd)
        {
            int hour, minute;
            string[] arr = time.Split(':');
            hour = int.Parse(arr[0]);
            minute = int.Parse(arr[1]);
            return AddHourToTime(hour, minute, hoursToAdd);
        }

        /// <summary>
        /// مقایسه دو ساعت و برگشت مقدار دقیقه اختلاف بین دو زمان
        /// </summary>
        /// <param name="time1">ساعت به صورت 24 ساعتی و بدون ثانیه</param>
        /// <param name="time2">ساعت به صورت 24 ساعتی و بدون ثانیه</param>
        /// <returns>مقدار دقیقه اختلاف بین دو زمان</returns>
        public static int CompareTime(string time1, string time2)
        {
            if (!TimeIsCorrect(time1))
                throw new Exception("ساعت نادرست است");
            if (!TimeIsCorrect(time2))
                throw new Exception("ساعت نادرست است");
            if (time1.CompareTo(time2) > 0)
            {
                int variance = 0;
                do
                {
                    variance--;
                    time2 = AddMinutesToTime(time2, 1);
                } while (time1.CompareTo(time2) > 0);
                return variance;
            }
            else if (time1.CompareTo(time2) < 0)
            {
                int variance = 0;
                do
                {
                    variance++;
                    time1 = AddMinutesToTime(time1, 1);
                } while (time1.CompareTo(time2) < 0);
                return variance;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// مقایسه دو ساعت و برگشت مقدار ساعت اختلاف بین دو زمان
        /// </summary>
        /// <param name="time1">ساعت به صورت 24 ساعتی و بدون ثانیه</param>
        /// <param name="time2">ساعت به صورت 24 ساعتی و بدون ثانیه</param>
        /// <returns>مقدار ساعت اختلاف بین دو زمان</returns>
        public static int CompareTimeWithHour(string time1, string time2)
        {
            if (!TimeIsCorrect(time1))
                throw new Exception("ساعت نادرست است");
            if (!TimeIsCorrect(time2))
                throw new Exception("ساعت نادرست است");
            if (time1.Split(':')[0].CompareTo(time2.Split(':')[0]) > 0)
            {
                int variance = 0;
                do
                {
                    variance--;
                    time2 = AddHourToTime(time2, 1);
                }
                while (time1.Split(':')[0].CompareTo(time2.Split(':')[0]) > 0);
                return variance;
            }
            else if (time1.Split(':')[0].CompareTo(time2.Split(':')[0]) < 0)
            {
                int variance = 0;
                do
                {
                    variance++;
                    time1 = AddHourToTime(time1, 1);
                }
                while (time1.Split(':')[0].CompareTo(time2.Split(':')[0]) < 0);
                return variance;
            }
            else
            {
                return 0;
            }
        }

        public static int GetSeason(DateTime dateTime)
        {
            switch (Calendar.GetMonth(dateTime))
            {
                case 1:
                case 2:
                case 3:
                    return 1;
                case 4:
                case 5:
                case 6:
                    return 2;
                case 7:
                case 8:
                case 9:
                    return 3;
                case 10:
                case 11:
                case 12:
                    return 4;
                default:
                    throw new Exception();
            }
        }

        public static string FixDate(string date)
        {
            char separator = '/';
            if (DateIsCorrect(date))
            {
                string[] arr = date.Split(separator);
                int y, m, d;
                y = int.Parse(arr[0]);
                m = int.Parse(arr[1]);
                d = int.Parse(arr[2]);
                if (y.ToString().Length == 2)
                    y = int.Parse("13" + y.ToString());
                return string.Format("{0}{3}{1}{3}{2}", y.ToString("####0000"), m.ToString("##00"), d.ToString("##00"), separator);
            }
            throw new Exception("فرمت تاریخ نادرست است");
        }

        /// <summary>
        /// کوچک کردن تاریخ
        /// 13
        /// و صفر های اضافی برداشته می شود
        /// </summary>
        /// <param name="date">تاریخ</param>
        /// <returns></returns>
        public static string GetShortDate(string date)
        {
            char separator = '/';
            if (DateIsCorrect(date))
            {
                string[] arr = date.Split(separator);
                string y, m, d;
                y = arr[0];
                m = arr[1];
                d = arr[2];
                if (y.Length == 4)
                    y = y.Substring(2, 2);
                if (m.Length == 2 && m[0] == '0')
                    m = m.Substring(1, 1);
                if (d.Length == 2 && d[0] == '0')
                    d = d.Substring(1, 1);
                return string.Format("{0}{3}{1}{3}{2}", y.ToString(), m.ToString(), d.ToString(), separator);
            }
            throw new Exception("فرمت تاریخ نادرست است");
        }
    }
}

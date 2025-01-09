using KermanBattery.Farmework.Domain.EntityDateTime;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KermanBattery.Farmework.Infrastructure.ConvertDateTime
{
    public static class GenericDateTime
    {

        //extensionmethod
        public static string PersianDateString(DateTime d)
        {
            CultureInfo faIR = new CultureInfo("fa-IR");
            return d.ToString("yyyy/MM/dd", faIR);
        }

        public static string DayOfWeekTitle(string dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case "Saturday": { dayOfWeek = "شنبه"; }; break;
                case "ُُSunday": { dayOfWeek = "یکشنبه"; }; break;
                case "Sunday": { dayOfWeek = "یکشنبه"; }; break;
                case "Monday": { dayOfWeek = "دوشنبه"; }; break;
                case "Tuesday": { dayOfWeek = "سشنبه"; }; break;
                case "Wednesday": { dayOfWeek = "چهارشنبه"; }; break;
                case "Thursday": { dayOfWeek = "پنجشنبه"; }; break;
                case "Friday": { dayOfWeek = "جمعه"; }; break;
                default: return "";
            }

            return dayOfWeek;
        }


        public static byte DayOfWeekIndex(string dayOfWeek)
        {
            byte res = 10;
            switch (dayOfWeek)
            {
                case "Saturday": { res = 1; }; break;
                case "ُُSunday": { res = 2; }; break;
                case "Sunday": { res = 2; }; break;
                case "Monday": { res = 3; }; break;
                case "Tuesday": { res = 4; }; break;
                case "Wednesday": { res = 5; }; break;
                case "Thursday": { res = 6; }; break;
                case "Friday": { res = 7; }; break;
                default: return 10;
            }

            return res;
        }


        public static ShamsiCurrentDate GetCurrentShamsiDate()
        {

            var dateTime = DateTime.Now;
            Calendar solarHijriCalendar = new PersianCalendar();
            var shamsiCurrentDate = new ShamsiCurrentDate();
            shamsiCurrentDate.Day = solarHijriCalendar.GetDayOfMonth(dateTime);
            shamsiCurrentDate.Month = solarHijriCalendar.GetMonth(dateTime);
            shamsiCurrentDate.Year = solarHijriCalendar.GetYear(dateTime);
            shamsiCurrentDate.MonthTitle = ConvertShamsiMonthNumberToTitle(shamsiCurrentDate.Month);
            shamsiCurrentDate.DayOfWeekTitle = DayOfWeekTitle(solarHijriCalendar.GetDayOfWeek(dateTime).ToString());
            shamsiCurrentDate.Description = shamsiCurrentDate.DayOfWeekTitle + " " + shamsiCurrentDate.Day + " " + shamsiCurrentDate.MonthTitle + " " + shamsiCurrentDate.Year;
            return shamsiCurrentDate;
        }



        public static DateTime GetFirstDayOfShamsiYear()
        {
            Calendar solarHijriCalendar = new PersianCalendar();
            return ConvertToGeorgianDate(solarHijriCalendar.GetYear(DateTime.Now) + "/01/01").Date;
        }


        public static DateTime ConvertToGeorgianDate(string shamsiDate)
        {
            int y, m, d;
            string[] dateList = new string[3];
            dateList = shamsiDate.Split('/');

            y = int.Parse(dateList[0]);
            m = int.Parse(dateList[1]);
            d = int.Parse(dateList[2]);

            PersianCalendar p = new PersianCalendar();
            return p.ToDateTime(y, m, d, 0, 0, 0, 0);
        }

        public static DateTime ConvertToGeorgianDatetime(string shamsiDate)
        {
            int year = Convert.ToInt32(shamsiDate.Substring(0, 4));
            int month = Convert.ToInt32(shamsiDate.Substring(5, 2));
            int day = Convert.ToInt32(shamsiDate.Substring(8, 2));
            int hour = Convert.ToInt32(shamsiDate.Substring(11, 2));
            int minute = Convert.ToInt32(shamsiDate.Substring(14, 2));
            int second = Convert.ToInt32(shamsiDate.Substring(17, 2));
            PersianCalendar p = new PersianCalendar();
            return p.ToDateTime(year, month, day, hour, minute, second, 0);
        }

        public static string ConvertShamsiMonthNumberToTitle(int month)
        {
            switch (month)
            {
                case 1: return "فررودين";
                case 2: return "ارديبهشت";
                case 3: return "خرداد";
                case 4: return "تير‏";
                case 5: return "مرداد";
                case 6: return "شهريور";
                case 7: return "مهر";
                case 8: return "آبان";
                case 9: return "آذر";
                case 10: return "دي";
                case 11: return "بهمن";
                case 12: return "اسفند";
                default: return "";
            }
        }



        #region MiladiToShamsi

        public static string ConvertToShamsiDateString(string gregorianDate)
        {
            return ConvertToShamsiDate(DateTime.Parse(gregorianDate));
        }


        public static string ConvertToShamsiDate(DateTime gregorianDate)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(gregorianDate).ToString("0000/") + pc.GetMonth(gregorianDate).ToString("00/") + pc.GetDayOfMonth(gregorianDate).ToString("00");
        }

        public static string ConvertToShamsiDateWithDash(DateTime gregorianDate)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(gregorianDate).ToString("0000-") + pc.GetMonth(gregorianDate).ToString("00-") +
                          pc.GetDayOfMonth(gregorianDate).ToString("00");
        }



        public static string ConvertToShamsiDateTime(DateTime gregorianDateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            var space = "\u0020";

            var res = pc.GetYear(gregorianDateTime).ToString("0000/") + pc.GetMonth(gregorianDateTime).ToString("00/") +
                           pc.GetDayOfMonth(gregorianDateTime).ToString("00 ") + space + pc.GetHour(gregorianDateTime).ToString("00:") + pc.GetMinute(gregorianDateTime).ToString("00:") + pc.GetSecond(gregorianDateTime).ToString("00");

            return res;
        }



        public static string ConvertToShamsiDateTimeWithDash(DateTime gregorianDateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            var space = "\u0020";

            return pc.GetYear(gregorianDateTime).ToString("0000-") + pc.GetMonth(gregorianDateTime).ToString("00-") +
                           pc.GetDayOfMonth(gregorianDateTime).ToString("00 ") + space + pc.GetHour(gregorianDateTime).ToString("00-") + pc.GetMinute(gregorianDateTime).ToString("00-") + pc.GetSecond(gregorianDateTime).ToString("00");

        }

        public static string ConvertToShamsiDateTime(string gregorianDateTime)
        {
            return ConvertToShamsiDateTime(DateTime.Parse(gregorianDateTime));
        }
        #endregion



        public static int SetDurationVacation(DateTime fromDateTime, DateTime toDateTime)
        {

            var diffDate = ((toDateTime - fromDateTime).TotalDays) + 1;

            List<DateTime> dateTimes = new();

            var durationMinute = 0;
            for (int i = 0; i < diffDate; i++)
            {
                var date = fromDateTime.AddDays(i);

                dateTimes.Add(date);

                PersianDateTime persianToDateTime = new PersianDateTime(date);

                if (persianToDateTime.DayOfWeek >= 0 && persianToDateTime.DayOfWeek < 5)
                {
                    // هشت ساعت معادل 480 دقیقه
                    durationMinute += 480;
                }

                if (persianToDateTime.DayOfWeek == 5)
                {
                    // چهار ساعت معادل 240 دقیقه
                    durationMinute += 240;
                }
            }

            return durationMinute;

        }


        public static string ConvertMinuteToHour(int minute)
        {

            if (minute < 0)
            {
                return $"منفی {Math.Abs(minute)} دقیقه";
            }


            //var time = TimeSpan.FromMinutes(minute);
            //string res = "";
            //if (time.TotalHours > 0)
            //{
            //    res += time.TotalHours + " ساعت ";
            //}

            //if(time.TotalMinutes > 0)
            //{
            //    res +=  time.TotalMinutes + " دقیقه ";
            //}

            TimeSpan spWorkMin = TimeSpan.FromMinutes(minute);
            string workHours = string.Format("{00}:{01}", (int)spWorkMin.TotalHours, spWorkMin.Minutes);


            return workHours;
        }


        public static ShamsiDiffDate ClacDiffShamsiDateType(DateTime inputDateTime, int monthCount)
        {

            var persianCalendar = new System.Globalization.PersianCalendar();

            var calcAddMonthToDateTime = persianCalendar.AddMonths(inputDateTime, monthCount);

            return new ShamsiDiffDate()
            {
                MiladiDate = calcAddMonthToDateTime,
                ShamsiDate = ConvertToShamsiDate(calcAddMonthToDateTime),
                DiffDay = (calcAddMonthToDateTime - inputDateTime).Days - 1,
            };

        }


        public static ConvertShamsiMonthToMiladiMonth ConvertShamsiMonthToMiladiMonth(int year, int month)
        {
            if (year.ToString().Length != 4 || (month > 12 || month < 1))
                return new ConvertShamsiMonthToMiladiMonth();

            var result = new ConvertShamsiMonthToMiladiMonth();
            int durationDate = 31;
            switch (month)
            {
                case 1:
                    {
                        result.Title = "فروردین";
                        break;
                    }
                case 2:
                    {
                        result.Title = "اردیبهشت";
                        break;
                    }
                case 3:
                    {
                        result.Title = "خرداد";
                        break;
                    }
                case 4:
                    {
                        result.Title = "تیر";
                        break;
                    }
                case 5:
                    {
                        result.Title = "مرداد";
                        break;
                    }
                case 6:
                    {
                        result.Title = "شهریور";
                        break;
                    }
                case 7:
                    {
                        result.Title = "مهر";
                        durationDate = 30;
                        break;
                    }
                case 8:
                    {
                        result.Title = "آبان";
                        durationDate = 30;
                        break;
                    }
                case 9:
                    {
                        result.Title = "آذر";
                        durationDate = 30;
                        break;
                    }
                case 10:
                    {
                        result.Title = "دی";
                        durationDate = 30;
                        break;
                    }
                case 11:
                    {
                        result.Title = "بهمن";
                        durationDate = 30;
                        break;
                    }
                case 12:
                    {
                        result.Title = "اسفند";
                        durationDate = (IsLeapYear(year)) ? 30 : 29;
                        break;
                    }
                default:
                    break;
            }
            result.StartDate = ConvertToGeorgianDate($"{year}/{month}/01");
            result.EndDate = result.StartDate.AddDays((durationDate - 1));
            result.DayDiff = durationDate;
            result.CalcDayDiff = (result.EndDate - result.StartDate).Days + 1;
            result.AllShamsiDateInMonth.AddRange(AllShamsiDateInMonth(result));



            return result;
        }


        public static List<DateAndValueDto> AllShamsiDateInMonth(ConvertShamsiMonthToMiladiMonth convertShamsiMonthToMiladiMonth)
        {
            List<DateAndValueDto> shmasiDate = new();

            List<WeekList> firstRow = new List<WeekList>();


            for (int i = 0; i < convertShamsiMonthToMiladiMonth.DayDiff; i++)
            {

                var date = convertShamsiMonthToMiladiMonth.StartDate.AddDays(i);
                var dayName = DayOfWeekTitle(date.DayOfWeek.ToString());
                var dayIndex = DayOfWeekIndex(date.DayOfWeek.ToString());
                shmasiDate.Add(new DateAndValueDto()
                {
                    Date = ConvertToShamsiDate(date),
                    Value = 0,
                    DayName = dayName,
                    DayIndex = dayIndex
                });



            }
            return shmasiDate;
        }

        public static bool IsLeapYear(int year)
        {
            PersianCalendar pc = new();
            return pc.IsLeapYear(year);
        }


    }
}

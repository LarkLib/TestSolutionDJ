using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace DJ.LMS.Utilities
{
    public class DateTimeHelper
    {
        private struct Struct8
        {
            public short short_0;
            public short short_1;
            public short short_2;
            public short short_3;
            public short short_4;
            public short short_5;
            public short short_6;
            public short short_7;
        }
        private DateTime dateTime_0 = DateTime.Now;
        /// <summary>    
        /// 获取某一年有多少周    
        /// </summary>    
        /// <param name="year">年份</param>    
        /// <returns>该年周数</returns>  
        public static int GetWeekAmount(int year)
        {
            DateTime time = new DateTime(year, 12, 31);
            GregorianCalendar gregorianCalendar = new GregorianCalendar();
            return gregorianCalendar.GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
        /// <summary>    
        /// 返回年度第几个星期   默认星期日是第一天    
        /// </summary>    
        /// <param name="date">时间</param>    
        /// <returns></returns> 
        public static int WeekOfYear(DateTime date)
        {
            GregorianCalendar gregorianCalendar = new GregorianCalendar();
            return gregorianCalendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }
        /// <summary>    
        /// 返回年度第几个星期    
        /// </summary>    
        /// <param name="date">时间</param>    
        /// <param name="week">一周的开始日期</param>    
        /// <returns></returns> 
        public static int WeekOfYear(DateTime date, DayOfWeek week)
        {
            GregorianCalendar gregorianCalendar = new GregorianCalendar();
            return gregorianCalendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, week);
        }
        /// <summary>    
        /// 得到一年中的某周的起始日和截止日    
        /// 年 nYear    
        /// 周数 nNumWeek    
        /// 周始 out dtWeekStart    
        /// 周终 out dtWeekeEnd    
        /// </summary>    
        /// <param name="nYear">年份</param>    
        /// <param name="nNumWeek">第几周</param>    
        /// <param name="dtWeekStart">开始日期</param>    
        /// <param name="dtWeekeEnd">结束日期</param> 
        public static void GetWeekTime(int nYear, int nNumWeek, out DateTime dtWeekStart, out DateTime dtWeekeEnd)
        {
            DateTime d = new DateTime(nYear, 1, 1);
            d += new TimeSpan((nNumWeek - 1) * 7, 0, 0, 0);
            dtWeekStart = d.AddDays((double)(-(int)d.DayOfWeek + DayOfWeek.Monday));
            dtWeekeEnd = d.AddDays((double)(DayOfWeek.Saturday - d.DayOfWeek + DayOfWeek.Monday));
        }
        /// <summary>    
        /// 得到一年中的某周的起始日和截止日    周一到周五  工作日    
        /// </summary>    
        /// <param name="nYear">年份</param>    
        /// <param name="nNumWeek">第几周</param>    
        /// <param name="dtWeekStart">开始日期</param>    
        /// <param name="dtWeekeEnd">结束日期</param> 
        public static void GetWeekWorkTime(int nYear, int nNumWeek, out DateTime dtWeekStart, out DateTime dtWeekeEnd)
        {
            DateTime d = new DateTime(nYear, 1, 1);
            d += new TimeSpan((nNumWeek - 1) * 7, 0, 0, 0);
            dtWeekStart = d.AddDays((double)(-(int)d.DayOfWeek + DayOfWeek.Monday));
            dtWeekeEnd = d.AddDays((double)(DayOfWeek.Saturday - d.DayOfWeek + DayOfWeek.Monday)).AddDays(-2.0);
        }
        [DllImport("kernel32.dll", EntryPoint = "SetLocalTime")]
        private static extern bool SetLocalTime_1(ref DateTimeHelper.Struct8 struct8_0);
        /// <summary>    
        /// 设置本地计算机时间    
        /// </summary>    
        /// <param name="dt">DateTime对象</param>
        public static void SetLocalTime(DateTime dt)
        {
            DateTimeHelper.Struct8 @struct;
            @struct.short_0 = (short)dt.Year;
            @struct.short_1 = (short)dt.Month;
            @struct.short_2 = (short)dt.DayOfWeek;
            @struct.short_3 = (short)dt.Day;
            @struct.short_4 = (short)dt.Hour;
            @struct.short_5 = (short)dt.Minute;
            @struct.short_6 = (short)dt.Second;
            @struct.short_7 = (short)dt.Millisecond;
            DateTimeHelper.SetLocalTime_1(ref @struct);
        }
        private static int smethod_0(string string_0, bool bool_0)
        {
            int result;
            if (string.IsNullOrEmpty(string_0))
            {
                result = 0;
            }
            else
            {
                string_0 = string_0.Trim();
                if (!bool_0)
                {
                    string pattern = "-?\\d+";
                    Regex regex = new Regex(pattern);
                    string_0 = regex.Match(string_0.Trim()).Value;
                }
                int num = 0;
                int.TryParse(string_0, out num);
                result = num;
            }
            return result;
        }
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DateTimeHelper()
        {
        }
        /// <summary>    
        /// 构造函数    
        /// </summary>    
        /// <param name="dateTime">时间</param> 
        public DateTimeHelper(DateTime dateTime)
        {
            this.dateTime_0 = dateTime;
        }
        /// <summary>    
        /// 构造函数（字符串时间）    
        /// </summary>    
        /// <param name="dateTime">时间</param> 
        public DateTimeHelper(string dateTime)
        {
            this.dateTime_0 = DateTime.Parse(dateTime);
        }
        /// <summary>    
        /// 基于给定（或当前）日期的偏移日期    
        /// </summary>    
        /// <param name="days">7天前:-7 7天后:+7</param>    
        /// <returns></returns> 
        public string GetTheDay(int? days)
        {
            int? num = days;
            int num2 = num.HasValue ? num.GetValueOrDefault() : 0;
            return this.dateTime_0.AddDays((double)num2).ToShortDateString();
        }
        /// <summary>    
        /// 周日    
        /// </summary>    
        /// <param name="weeks">上周-1 下周+1 本周0</param>    
        /// <returns></returns> 
        public string GetSunday(int? weeks)
        {
            int? num = weeks;
            int num2 = num.HasValue ? num.GetValueOrDefault() : 0;
            return this.dateTime_0.AddDays(Convert.ToDouble((int)(-(int)Convert.ToInt16(this.dateTime_0.DayOfWeek))) + (double)(7 * num2)).ToShortDateString();
        }
        /// <summary>    
        /// 周六    
        /// </summary>    
        /// <param name="weeks">上周-1 下周+1 本周0</param>    
        /// <returns></returns>
        public string GetSaturday(int? weeks)
        {
            int? num = weeks;
            int num2 = num.HasValue ? num.GetValueOrDefault() : 0;
            return this.dateTime_0.AddDays(Convert.ToDouble((int)(6 - Convert.ToInt16(this.dateTime_0.DayOfWeek))) + (double)(7 * num2)).ToShortDateString();
        }
        /// <summary>    
        /// 月第一天    
        /// </summary>    
        /// <param name="months">上月-1 本月0 下月1</param>    
        /// <returns></returns> 
        public string GetFirstDayOfMonth(int? months)
        {
            int? num = months;
            int months2 = num.HasValue ? num.GetValueOrDefault() : 0;
            DateTime dateTime = DateTime.Now;
            dateTime = DateTime.Parse(dateTime.ToString("yyyy-MM-01"));
            dateTime = dateTime.AddMonths(months2);
            return dateTime.ToShortDateString();
        }
        /// <summary>    
        /// 月最后一天    
        /// </summary>    
        /// <param name="months">上月0 本月1 下月2</param>    
        /// <returns></returns> 
        public string GetLastDayOfMonth(int? months)
        {
            int? num = months;
            int months2 = num.HasValue ? num.GetValueOrDefault() : 0;
            DateTime dateTime = DateTime.Parse(this.dateTime_0.ToString("yyyy-MM-01"));
            dateTime = dateTime.AddMonths(months2);
            dateTime = dateTime.AddDays(-1.0);
            return dateTime.ToShortDateString();
        }
        /// <summary>    
        /// 年度第一天    
        /// </summary>    
        /// <param name="years">上年度-1 下年度+1</param>    
        /// <returns></returns> 
        public string GetFirstDayOfYear(int? years)
        {
            int? num = years;
            int value = num.HasValue ? num.GetValueOrDefault() : 0;
            DateTime dateTime = DateTime.Parse(this.dateTime_0.ToString("yyyy-01-01"));
            dateTime = dateTime.AddYears(value);
            return dateTime.ToShortDateString();
        }
        /// <summary>    
        /// 年度最后一天    
        /// </summary>    
        /// <param name="years">上年度0 本年度1 下年度2</param>    
        /// <returns></returns> 
        public string GetLastDayOfYear(int? years)
        {
            int? num = years;
            int value = num.HasValue ? num.GetValueOrDefault() : 0;
            DateTime dateTime = DateTime.Parse(this.dateTime_0.ToString("yyyy-01-01"));
            dateTime = dateTime.AddYears(value);
            dateTime = dateTime.AddDays(-1.0);
            return dateTime.ToShortDateString();
        }
        /// <summary>    
        /// 季度第一天    
        /// </summary>    
        /// <param name="quarters">上季度-1 下季度+1</param>    
        /// <returns></returns> 
        public string GetFirstDayOfQuarter(int? quarters)
        {
            int? num = quarters;
            int num2 = num.HasValue ? num.GetValueOrDefault() : 0;
            return this.dateTime_0.AddMonths(num2 * 3 - (this.dateTime_0.Month - 1) % 3).ToString("yyyy-MM-01");
        }
        /// <summary>    
        /// 季度最后一天    
        /// </summary>    
        /// <param name="quarters">上季度0 本季度1 下季度2</param>    
        /// <returns></returns> 
        public string GetLastDayOfQuarter(int? quarters)
        {
            int? num = quarters;
            int num2 = num.HasValue ? num.GetValueOrDefault() : 0;
            DateTime dateTime = this.dateTime_0.AddMonths(num2 * 3 - (this.dateTime_0.Month - 1) % 3);
            dateTime = DateTime.Parse(dateTime.ToString("yyyy-MM-01"));
            dateTime = dateTime.AddDays(-1.0);
            return dateTime.ToShortDateString();
        }
        /// <summary>    
        /// 中文星期    
        /// </summary>    
        /// <returns></returns>
        public string GetDayOfWeekCN()
        {
            string[] array = new string[]
			{
				"星期日", 
				"星期一", 
				"星期二", 
				"星期三", 
				"星期四", 
				"星期五", 
				"星期六"
			};
            return array[(int)Convert.ToInt16(this.dateTime_0.DayOfWeek)];
        }
        /// <summary>    
        /// 获取星期数字形式,周一开始    
        /// </summary>    
        /// <returns></returns>
        public int GetDayOfWeekNum()
        {
            return (Convert.ToInt16(this.dateTime_0.DayOfWeek) == 0) ? 7 : ((int)Convert.ToInt16(this.dateTime_0.DayOfWeek));
        }
        /// <summary>    
        /// C#的时间到Javascript的时间的转换    
        /// </summary>    
        /// <param name="TheDate"></param>    
        /// <returns></returns> 
        public static long ConvertTimeToJS(DateTime TheDate)
        {
            DateTime dateTime = new DateTime(1970, 1, 1);
            TimeSpan timeSpan = new TimeSpan(TheDate.ToUniversalTime().Ticks - dateTime.Ticks);
            return (long)timeSpan.TotalMilliseconds;
        }
        /// <summary>    
        /// PHP的时间转换成C#中的DateTime    
        /// </summary>    
        /// <param name="time"></param>    
        /// <returns></returns>
        public static DateTime ConvertPHPToTime(long time)
        {
            DateTime dateTime = new DateTime(1970, 1, 1);
            long ticks = (time + 28800L) * 10000000L + dateTime.Ticks;
            DateTime result = new DateTime(ticks);
            return result;
        }
        /// <summary>    
        ///  C#中的DateTime转换成PHP的时间    
        /// </summary>    
        /// <param name="time"></param>    
        /// <returns></returns>
        public static long ConvertTimeToPHP(DateTime time)
        {
            DateTime dateTime = new DateTime(1970, 1, 1);
            return (DateTime.UtcNow.Ticks - dateTime.Ticks) / 10000000L;
        }
        /// <summary>    
        /// 获取两个时间的时间差    
        /// </summary>    
        /// <param name="beginTime">开始时间</param>    
        /// <param name="endTime">结束时间</param>    
        /// <returns></returns>
        public static string GetDiffTime(DateTime beginTime, DateTime endTime)
        {
            int num = 0;
            return DateTimeHelper.GetDiffTime(beginTime, endTime, ref num);
        }
        /// <summary>    
        /// 计算2个时间差    
        /// </summary>    
        /// <param name="beginTime">开始时间</param>    
        /// <param name="endTime">结束时间</param>    
        /// <param name="mindTime">提醒时间</param>    
        /// <returns></returns> 
        public static string GetDiffTime(DateTime beginTime, DateTime endTime, ref int mindTime)
        {
            string text = string.Empty;
            int num = Convert.ToInt32(endTime.Subtract(beginTime).TotalSeconds);
            int num2 = 60;
            int num3 = 60 * 60;
            int num4 = 3600 * 24;
            int num5 = 86400 * 30;
            int num6 = 2592000 * 12;
            if (mindTime > num && num > 0)
            {
                mindTime = 1;
            }
            else
            {
                mindTime = 0;
            }
            if (num > num6)
            {
                text = text + num / num6 + "年";
                num %= num6;
            }
            if (num > num5)
            {
                text = text + num / num5 + "月";
                num %= num5;
            }
            if (num > num4)
            {
                text = text + num / num4 + "天";
                num %= num4;
            }
            if (num > num3)
            {
                text = text + num / num3 + "小时";
                num %= num3;
            }
            if (num > num2)
            {
                text = text + num / num2 + "分";
                num %= num2;
            }
            text = text + num + "秒";
            return text;
        }
        /// <summary>    
        /// 获得两个日期的间隔    
        /// </summary>    
        /// <param name="DateTime1">日期一。</param>    
        /// <param name="DateTime2">日期二。</param>    
        /// <returns>日期间隔TimeSpan。</returns> 
        public static TimeSpan GetDiffTime2(DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan timeSpan = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts = new TimeSpan(DateTime2.Ticks);
            return timeSpan.Subtract(ts).Duration();
        }
        /// <summary>    
        /// 得到随机日期    
        /// </summary>    
        /// <param name="time1">起始日期</param>    
        /// <param name="time2">结束日期</param>    
        /// <returns>间隔日期之间的 随机日期</returns>  
        public static DateTime GetRandomTime(DateTime time1, DateTime time2)
        {
            Random random = new Random();
            DateTime dateTime = default(DateTime);
            //DateTime dateTime2 = default(DateTime);
            TimeSpan timeSpan = new TimeSpan(time1.Ticks - time2.Ticks);
            double totalSeconds = timeSpan.TotalSeconds;
            int num = 0;
            if (totalSeconds > 2147483647.0)
            {
                num = 2147483647;
            }
            else
            {
                if (totalSeconds < -2147483648.0)
                {
                    num = -2147483648;
                }
                else
                {
                    num = (int)totalSeconds;
                }
            }
            DateTime result;
            if (num > 0)
            {
                dateTime = time2;
            }
            else
            {
                if (num >= 0)
                {
                    result = time1;
                    return result;
                }
                dateTime = time1;
            }
            int value = num;
            if (num <= -2147483648)
            {
                value = -2147483647;
            }
            int num2 = random.Next(Math.Abs(value));
            result = dateTime.AddSeconds((double)num2);
            return result;
        }
    }
}

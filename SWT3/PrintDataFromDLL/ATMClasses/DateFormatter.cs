using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMClasses
{
    public class DateFormatter
    {
        public static string FormatTimestamp(string timestamp)
        {
            string format = "yyyyMMddHHmmssfff";    //Set input format
            DateTime date = DateTime.ParseExact(timestamp, format, CultureInfo.CreateSpecificCulture("en-US"));
            string dateformat = String.Format(new CultureInfo("en-US"), "{0:MMMM d}{1}{0:, yyyy, 'at' HH:mm:ss 'and' fff 'miliseconds'}", date, AddDaySuffix(date));   //Format date correctly

            return dateformat;
        }

        private static string AddDaySuffix(DateTime timeStamp)
        {
            //returns "st", "nd", "rd" or "th"
            return (timeStamp.Day % 10 == 1 && timeStamp.Day != 11) ? "st"
                : (timeStamp.Day % 10 == 2 && timeStamp.Day != 12) ? "nd"
                : (timeStamp.Day % 10 == 3 && timeStamp.Day != 13) ? "rd"
                : "th";
        }
    }
}

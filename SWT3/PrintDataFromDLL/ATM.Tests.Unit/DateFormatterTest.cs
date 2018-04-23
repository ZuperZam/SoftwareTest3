using System;
using System.Collections.Generic;
using ATMClasses;
using ATMClasses.Interfaces;
using NUnit.Framework;

namespace ATM.Tests.Unit
{
    [TestFixture]
    public class DateFormatterTest
    {
        IDateFormatter _uut;
        [SetUp]
        public void Setup()
        {
            _uut = new DateFormatter();
        }

        //Tests a good variety of suffixes. Tests 1-3, 11-13, 21-23, 31. All with boundaries as well
        [TestCase("01", "st")]
        [TestCase("02", "nd")]
        [TestCase("03", "rd")]
        [TestCase("04", "th")]
        [TestCase("10", "th")]
        [TestCase("11", "th")]
        [TestCase("12", "th")]
        [TestCase("13", "th")]
        [TestCase("14", "th")]
        [TestCase("20", "th")]
        [TestCase("21", "st")]
        [TestCase("22", "nd")]
        [TestCase("23", "rd")]
        [TestCase("24", "th")]
        [TestCase("30", "th")]
        [TestCase("31", "st")]
        public void ReturnsCorrectDaySuffix(string dayNumber, string expectedSuffix)
        {
            string shortDayNumber = string.Format("{0}", int.Parse(dayNumber));  //removes '0' from low daynumbers
            string expectedTimestamp = $"October {shortDayNumber}{expectedSuffix}, 2015, at 21:34:56 and 789 milliseconds";

            Assert.AreEqual(expectedTimestamp, _uut.FormatTimestamp($"201510{dayNumber}213456789"));
        }

        [TestCase("01", "January")]
        [TestCase("02", "February")]
        [TestCase("03", "March")]
        [TestCase("04", "April")]
        [TestCase("05", "May")]
        [TestCase("06", "June")]
        [TestCase("07", "July")]
        [TestCase("08", "August")]
        [TestCase("09", "September")]
        [TestCase("10", "October")]
        [TestCase("11", "November")]
        [TestCase("12", "December")]
        public void ReturnsCorrectMonthName(string monthNumber, string expectedMonthName)
        {
            string expectedTimestamp = $"{expectedMonthName} 1st, 2015, at 21:34:56 and 789 milliseconds";

            Assert.AreEqual(expectedTimestamp, _uut.FormatTimestamp($"2015{monthNumber}01213456789"));
        }
    }
}

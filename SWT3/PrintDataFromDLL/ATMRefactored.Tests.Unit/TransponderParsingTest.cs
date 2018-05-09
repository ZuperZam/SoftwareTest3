using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMRefactored;
using ATMRefactored.Interfaces;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace ATMRefactored.Tests.Unit
{
	
    [TestFixture]
    public class TransponderParsingTest
    {
		public TransponderParsing _uut;
        public ITransponderReceiver receiver;
        private List<string> testList = new List<string>();
        private string Tag = "ATR423";
        private string X = "39045";
        private string Y = "12932";
        private string Alt = "14000";
        private string Timestamp = "20151006213456789";
        private List<string> _transponderArgsList;
        private RawTransponderDataEventArgs _transponderDataEventArgs;
        [SetUp]
	    public void Setup()
        {
            
	        receiver = Substitute.For<ITransponderReceiver>();
            _uut = new TransponderParsing(receiver);
	        testList.Add(Tag);
	        testList.Add(X);
	        testList.Add(Y);
	        testList.Add(Alt);
	        testList.Add(Timestamp);

            _transponderArgsList = new List<string> { "ATR423;39045;12932;14000;20151006213456789" };
            _transponderDataEventArgs = new RawTransponderDataEventArgs(_transponderArgsList);

        }

        public void RaiseFakeTransponderReceiverEvent()
        {
            receiver.TransponderDataReady += Raise.EventWith(_transponderDataEventArgs);    //Raises a fake event with the stated arguments
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

        [Test]
        public void AreListsIdentical()
        {
            string test = "ATR423;39045;12932;14000;20151006213456789";
			Assert.That(_uut.TransponderParser(test), Is.EqualTo(testList));
        }

        [Test]
        public void AreNotEqualList()
        {
            string test = "MAR123;39045;12932;14000;20151006213456789";
			Assert.AreNotEqual(_uut.TransponderParser(test), (testList));
        }

		[Test]
        public void MakeTrack_AddsTrackObject_CorrectTag()
        {
			RaiseFakeTransponderReceiverEvent();
            Assert.That(_uut.trackObjects[0].Tag, Is.EqualTo("ATR423"));

        }

        [Test]
        public void MakeTrack_AddsTrackObject_CorrectXCoord()
        {
            RaiseFakeTransponderReceiverEvent();
            Assert.That(_uut.trackObjects[0].XCoord, Is.EqualTo(39045));

        }

        [Test]
        public void MakeTrack_AddsTrackObject_CorrectYCoord()
        {
            RaiseFakeTransponderReceiverEvent();
            Assert.That(_uut.trackObjects[0].YCoord, Is.EqualTo(12932));

        }

        [Test]
        public void MakeTrack_AddsTrackObject_CorrectAltitude()
        {
            RaiseFakeTransponderReceiverEvent();
            Assert.That(_uut.trackObjects[0].Altitude, Is.EqualTo(14000));

        }

        [Test]
        public void MakeTrack_AddsTrackObject_CorrectTimestamp()
        {
            RaiseFakeTransponderReceiverEvent();
            
            Assert.That(_uut.trackObjects[0].PrettyTimeStamp, Is.EqualTo("October 6th, 2015, at 21:34:56 and 789 milliseconds"));

        }

    }

	

    
}

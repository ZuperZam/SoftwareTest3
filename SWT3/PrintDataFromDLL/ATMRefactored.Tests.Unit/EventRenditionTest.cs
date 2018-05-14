using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMRefactored;
using ATMRefactored.Interfaces;
using Castle.Core.Internal;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TransponderReceiver;


namespace ATMRefactored.Tests.Unit
{
    [TestFixture]
    public class EventRenditionTest
    {
        private IEventRendition _uut;
        TrackObject trackobject1;
        TrackObject trackobject2;
        List<String> list1;
        List<String> list2;
        List<TrackObject> trackObjectList;
        [SetUp]
        public void Setup()
        {
            _uut = new EventRendition();
            list1 = new List<string> {"MAR123", "50000", "50000", "1000", "20151006213456789"};
            list2 = new List<string> {"FRE123", "50000", "50000", "1000", "20151006213456789"};
            trackobject1 = new TrackObject(list1);
            trackobject2 = new TrackObject(list2);
            trackObjectList = new List<TrackObject>(){trackobject1, trackobject2};
        }

        [Test]
        public void RenderEvents_LogsEvent()
        {
            _uut.RenderEvents(trackObjectList);
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {

                

                string[] lines = System.IO.File.ReadAllLines(@"SeparatationEventLog.txt");

                Assert.AreEqual("Timestamp: 06-10-2015 21:34:56\tMAR123 and FRE123 are breaking separation rules", lines[0]);
            }
        }

        //[Test]
        //public void LogSeparationEvent_LogsEvent_ToFile()
        //{
            
        //    using (var stream = new MemoryStream())
        //    using (var writer = new StreamWriter(stream))
        //    {
                
        //        _uut.LogSeparationEvent(trackobject1, trackobject2);
                
        //        string[] lines = System.IO.File.ReadAllLines(@"SeparatationEventLog.txt");
                
        //        Assert.AreEqual("Timestamp: 06-10-2015 21:34:56\tMAR123 and FRE123 are breaking separation rules", lines[0]);
        //    }
        //}

        //Horizontal, no altitude difference
        [TestCase(5000, 9999, 1000, 1000)]
        [TestCase(9999, 5000, 1000, 1000)]
        public void InsideOtherAirspaceNoAltitudeDifference_ReturnsTrue(int xCoordTO1, int xCoordTO2, int altitudeTO1,
            int altitudeTO2)
        {
            trackobject1.XCoord = xCoordTO1;
            trackobject1.Altitude = altitudeTO1;
            trackobject2.XCoord = xCoordTO2;
            trackobject2.Altitude = altitudeTO2;

            Assert.That(_uut.IsInOtherAirSpace(trackobject1, trackobject2), Is.EqualTo(true));
        }

        //Horizontal, no altitude difference
        [TestCase(5000, 10000, 1000, 1000)]
        [TestCase(10000, 5000, 1000, 1000)]
        public void OutsideOtherAirspaceNoAltitudeDifference_ReturnsFalse(int xCoordTO1, int xCoordTO2, int altitudeTO1,
            int altitudeTO2)
        {
            trackobject1.XCoord = xCoordTO1;
            trackobject1.Altitude = altitudeTO1;
            trackobject2.XCoord = xCoordTO2;
            trackobject2.Altitude = altitudeTO2;

            Assert.That(_uut.IsInOtherAirSpace(trackobject1, trackobject2), Is.EqualTo(false));
        }

        //No horizontal difference, big altitude difference.
        [TestCase(5000, 5000, 1000, 1299)]
        [TestCase(5000, 5000, 1299, 1000)]
        public void InsideHorizontalAirspaceBigAltitudeDifference_ReturnsTrue(int xCoordTO1, int xCoordTO2,
            int altitudeTO1,
            int altitudeTO2)
        {
            trackobject1.XCoord = xCoordTO1;
            trackobject1.Altitude = altitudeTO1;
            trackobject2.XCoord = xCoordTO2;
            trackobject2.Altitude = altitudeTO2;

            Assert.That(_uut.IsInOtherAirSpace(trackobject1, trackobject2), Is.EqualTo(true));
        }

        //No horizontal difference, big altitude difference.
        [TestCase(5000, 5000, 1000, 1300)]
        [TestCase(5000, 5000, 1300, 1000)]
        public void InsideHorizontalAirspaceBigAltitudeDifference_ReturnsFalse(int xCoordTO1, int xCoordTO2,
            int altitudeTO1,
            int altitudeTO2)
        {
            trackobject1.XCoord = xCoordTO1;
            trackobject1.Altitude = altitudeTO1;
            trackobject2.XCoord = xCoordTO2;
            trackobject2.Altitude = altitudeTO2;

            Assert.That(_uut.IsInOtherAirSpace(trackobject1, trackobject2), Is.EqualTo(false));


        }

        public void Is1DDistanceCorrect(int x1, int x2, int result)
        {
            result = Math.Abs(x1 - x2);
            Assert.That(_uut.CalculateDistance1D(x1, x2), Is.EqualTo(result));
        }

        [TestCase(5, 10, 5, 10, 7)]
        [TestCase(10, 5, 10, 5, 7)]
        [TestCase(-5, 10, 5, -10, 450)]
        [TestCase(5, -10, -5, 10, -450)]
        public void Is2DDistanceCorrect(int x1, int x2, int y1, int y2, double result)
        {
            Int64 xDist = _uut.CalculateDistance1D(x1, x2);
            Int64 yDist = _uut.CalculateDistance1D(y1, y2);
            result = Math.Sqrt((xDist * xDist) + (yDist * yDist));
            Assert.That(_uut.CalculateDistance2D(x1, x2, y1, y2), Is.EqualTo(result));
        }
    }
}

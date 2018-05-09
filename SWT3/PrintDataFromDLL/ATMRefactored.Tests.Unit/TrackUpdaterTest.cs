using System;
using System.Collections.Generic;
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
    public class TrackUpdaterTest
    {
        TrackObject trackobject1;
        TrackObject trackobject2;
        TrackObject trackobject3;
        List<String> list1;
        List<String> list2;
        List<String> list3;
        List<TrackObject> trackObjectList;
        int x1, x2, y1, y2;

        private ITrackUpdater _uut;
        [SetUp]
        public void Setup()
        {
            _uut = new TrackUpdater();
            list1 = new List<string> { "MAR123", "50000", "50000", "1000", "20151006213456789" };
            list2 = new List<string> { "FRE595", "50000", "50000", "1000", "20151006213456789" };
            list3 = new List<string> { "FRE595", "50050", "50050", "980", "20151006213458800" };
            trackobject1 = new TrackObject(list1);
            trackobject2 = new TrackObject(list2);
            trackobject3 = new TrackObject(list3);
            trackObjectList = new List<TrackObject>(){trackobject3};
        }

        [Test]
        public void UpdatedTracksAreAddedToList()
        {
            _uut.UpdateTracks(trackObjectList);
            Assert.That(_uut._oldTrackObjects.Count, Is.EqualTo(1));
        }

        [Test]
        public void NewTrackObjectIsAddedToOldList()
        {
            _uut._oldTrackObjects.Add(trackobject1);
            _uut.UpdateTracks(trackObjectList);
            Assert.That(_uut._oldTrackObjects[0], Is.EqualTo(trackobject3));
        }

        [Test]
        public void UpdateTracks_CalculateVelocity_IsCorrect()
        {

            _uut._oldTrackObjects.Add(trackobject2);
            _uut.UpdateTracks(trackObjectList);
            Assert.That(_uut._oldTrackObjects[0].Velocity, Is.EqualTo(35));
        }

        [Test]
        public void UpdateTracks_CalculateCourse_IsCorrect()
        {

            _uut._oldTrackObjects.Add(trackobject2);
            _uut.UpdateTracks(trackObjectList);
            Assert.That(_uut._oldTrackObjects[0].Course, Is.EqualTo(45));
        }


        [TestCase(5000, 5000, 5000, 5100, 0)]
        [TestCase(5000, 5002, 5000, 5100, 1)]
        [TestCase(5000, 5000, 5000, 5000, 90)]
        [TestCase(5000, 5100, 5000, 5100, 45)]
        [TestCase(5000, 5100, 5000, 5000, 90)]
        [TestCase(5000, 5100, 5100, 5000, 135)]
        [TestCase(5000, 5000, 5100, 5000, 180)]
        [TestCase(5100, 5000, 5100, 5000, 225)]
        [TestCase(5100, 5000, 5000, 5000, 270)]
        [TestCase(5100, 5000, 5000, 5100, 315)]
        [TestCase(5000, 4999, 5000, 5100, 359)]
        public void IsCourseCorrect(int x1, int x2, int y1, int y2, int result)
        {
            trackobject1.XCoord = x1;
            trackobject1.YCoord = y1;
            trackobject2.XCoord = x2;
            trackobject2.YCoord = y2;

            Assert.That(_uut.CalculateCourse(trackobject1, trackobject2), Is.EqualTo(result));
        }

        [TestCase(5000, 5000, 5000, 5100, "20151006213456000", "20151006213457000", 100)]
        [TestCase(5000, 5000, 5000, 5100, "20151006213456000", "20151006213456500", 200)]
        [TestCase(5000, 5000, 5000, 5100, "20151006213456000", "20151006213456001", 100000)]
        [TestCase(5000, 5000, 5000, 5100, "20151006213456000", "20151006213456001", 100000)]
        [TestCase(5000, 5100, 5000, 5100, "20151006213456000", "20151006213457000", 141)]
        [TestCase(5000, 5100, 5000, 5100, "20151006213456000", "20151006213456500", 282)]
        [TestCase(5100, 5000, 5100, 5000, "20151006213456000", "20151006213457000", 141)]
        [TestCase(5100, 5000, 5100, 5000, "20151006213456000", "20151006213456500", 282)]
        public void IsVelocityCorrect(int x1, int x2, int y1, int y2, string timestamp1, string timestamp2, int result)
        {
            trackobject1.XCoord = x1;
            trackobject1.YCoord = y1;
            trackobject2.XCoord = x2;
            trackobject2.YCoord = y2;

            trackobject1.Timestamp = DateTime.ParseExact(timestamp1, "yyyyMMddHHmmssfff",
                System.Globalization.CultureInfo.InvariantCulture);
            trackobject2.Timestamp = DateTime.ParseExact(timestamp2, "yyyyMMddHHmmssfff",
                System.Globalization.CultureInfo.InvariantCulture);

            Assert.That(_uut.CalculateVelocity(trackobject1, trackobject2), Is.EqualTo(result));
        }

        [TestCase(5, 10, 5)]
        [TestCase(10, 5, 5)]
        [TestCase(-5, 10, 5)]
        [TestCase(5, -10, 15)]

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

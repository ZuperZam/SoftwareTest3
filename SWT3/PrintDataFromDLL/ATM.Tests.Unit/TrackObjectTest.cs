using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMClasses;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ATM.Tests.Unit
{
    [TestFixture()]
    class TrackObjectTest
    {
        private TrackObject _uut;

        private List<string> trackData;
        private string _tag;
        private int _xCoord;
        private int _yCoord;
        private int _altitude;
        private int _velocity;
        private int _course;
        private string _prettyTimestamp;
        private DateTime _timestamp;

        [SetUp]
        public void SetUp()
        {
            trackData = new List<string> { "ATR423", "50000", "50000", "1000", "20151006213456789" };
            _uut = new TrackObject(trackData);
            _uut.Velocity = 10;
            _uut.Course = 100;
            _uut.PrettyTimeStamp = "October 6th, 2015, at 21:34:56 and 789 milliseconds";

            _tag = "ATR423";
            _xCoord = 50000;
            _yCoord = 50000;
            _altitude = 1000;
            _velocity = 10;
            _course = 100;
            _prettyTimestamp = "October 6th, 2015, at 21:34:56 and 789 milliseconds";
            _timestamp = DateTime.ParseExact("20151006213456789", "yyyyMMddHHmmssfff",
                System.Globalization.CultureInfo.InvariantCulture);
        }

        [Test]
        public void Track_SetTag_ReturnsExpectedTag()
        {
            Assert.AreEqual(_tag, _uut.Tag);
        }

        [Test]
        public void Track_SetXCoord_ReturnsExpectedXCoord()
        {
            Assert.AreEqual(_xCoord, _uut.XCoord);
        }

        [Test]
        public void Track_SetYCoord_ReturnsExpectedYCoord()
        {
            Assert.AreEqual(_yCoord, _uut.YCoord);
        }

        [Test]
        public void Track_SetAltitude_ReturnsExpectedAltitude()
        {
            Assert.AreEqual(_altitude, _uut.Altitude);
        }

        [Test]
        public void Track_SetVelocity_ReturnsExpectedVelocity()
        {
            Assert.AreEqual(_velocity, _uut.Velocity);
        }

        [Test]
        public void Track_SetCourse_ReturnsExpectedCourse()
        {
            Assert.AreEqual(_course, _uut.Course);
        }

        [Test]
        public void Track_SetTimestamp_ReturnsExpectedTimestamp()
        {
            Assert.AreEqual(_timestamp, _uut.Timestamp);
        }

        [Test]
        public void Track_SetPrettyTimestamp_ReturnsExpectedPrettyTimestamp()
        {
            Assert.AreEqual(_prettyTimestamp, _uut.PrettyTimeStamp);
        }
    }
}

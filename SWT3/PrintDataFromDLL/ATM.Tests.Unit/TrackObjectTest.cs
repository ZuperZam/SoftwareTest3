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
        private string _xCoord;
        private string _yCoord;
        private string _altitude;
        private string _timestamp;

        [SetUp]
        public void SetUp()
        {
            trackData = new List<string> { "ATR423", "50000", "50000", "1000", "October 6th, 2015, at 21:34:56 and 789 milliseconds" };
            _uut = new TrackObject(trackData);

            _tag = "ATR423";
            _xCoord = "50000";
            _yCoord = "50000";
            _altitude = "1000";
            _timestamp = "October 6th, 2015, at 21:34:56 and 789 milliseconds";
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
        public void Track_SetTimestamp_ReturnsExpectedTimestamp()
        {
            Assert.AreEqual(_timestamp, _uut.Timestamp);
        }
    }
}

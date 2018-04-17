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
        private DateTime _timestamp;

        [SetUp]
        public void SetUp()
        {
            trackData = new List<string> { "ATR423", "50000", "50000", "1000", "20151006213456789" };
            _uut = new TrackObject(trackData);

            _tag = "ATR423";
            _xCoord = 50000;
            _yCoord = 50000;
            _altitude = 1000;
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
        public void Track_SetTimestamp_ReturnsExpectedTimestamp()
        {
            Assert.AreEqual(_timestamp, _uut.Timestamp);
        }
    }
}

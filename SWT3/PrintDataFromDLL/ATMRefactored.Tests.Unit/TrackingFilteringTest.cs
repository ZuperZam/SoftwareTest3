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
    public class TrackingFilteringTest
    {
        
        private const int MinCoord = 10000;
        private const int MaxCoord = 90000;
        private const int MinAltitude = 500;
        private const int MaxAltitude = 20000;
        List<string> trackData;
        private TrackObject trackObject;
        private List<TrackObject> trackObjects;
        private ITrackingFiltering _uut; 
        [SetUp]
        public void Setup()
        {
            _uut = new TrackingFiltering();
            trackData = new List<string> { "ATR423", "50000", "50000", "1000", "20151006213456789" };
            trackObject = new TrackObject(trackData);
            
        }


        [Test]
        public void TrackIsRemoved_XCoord_OutofBounds()
        {
            trackObject.XCoord = MaxCoord + 1;
            trackObjects = new List<TrackObject>();
            trackObjects.Add(trackObject);
            _uut.IsTrackInMonitoredAirspace(trackObjects);

            Assert.That(trackObjects.Count, Is.EqualTo(1));
        }
        //X Coords
        [Test]
        public void XCoordinateInsideUpperBoundary_ReturnsTrue()
        {

            trackObject.XCoord = MaxCoord;
            Assert.That(_uut.ValidateCoordinate(trackObject.XCoord, trackObject.YCoord), Is.EqualTo(true));
        }

        [Test]
        public void XCoordinateOutsideUpperBoundary_ReturnsFalse()
        {

            trackObject.XCoord = MaxCoord + 1;
            Assert.That(_uut.ValidateCoordinate(trackObject.XCoord, trackObject.YCoord), Is.EqualTo(false));
        }

        [Test]
        public void XCoordinateInsideLowerBoundary_ReturnsTrue()
        {

            trackObject.XCoord = MinCoord;
            Assert.That(_uut.ValidateCoordinate(trackObject.XCoord, trackObject.YCoord), Is.EqualTo(true));
        }

        [Test]
        public void XCoordinateOutsideLowerBoundary_ReturnsFalse()
        {

            trackObject.XCoord = MinCoord - 1;
            Assert.That(_uut.ValidateCoordinate(trackObject.XCoord, trackObject.YCoord), Is.EqualTo(false));
        }

        //Y Coords

        [Test]
        public void YCoordinateInsideUpperBoundary_ReturnsTrue()
        {

            trackObject.YCoord = MaxCoord;
            Assert.That(_uut.ValidateCoordinate(trackObject.XCoord, trackObject.YCoord), Is.EqualTo(true));
        }

        [Test]
        public void YCoordinateOutsideUpperBoundary_ReturnsFalse()
        {

            trackObject.YCoord = MaxCoord + 1;
            Assert.That(_uut.ValidateCoordinate(trackObject.XCoord, trackObject.YCoord), Is.EqualTo(false));
        }

        [Test]
        public void YCoordinateInsideLowerBoundary_ReturnsTrue()
        {

            trackObject.YCoord = MinCoord;
            Assert.That(_uut.ValidateCoordinate(trackObject.XCoord, trackObject.YCoord), Is.EqualTo(true));
        }

        [Test]
        public void YCoordinateOutsideLowerBoundary_ReturnsFalse()
        {

            trackObject.YCoord = MinCoord - 1;
            Assert.That(_uut.ValidateCoordinate(trackObject.XCoord, trackObject.YCoord), Is.EqualTo(false));
        }

        //Altitude
        [Test]
        public void AltitudeInsideUpperBoundary_ReturnsTrue()
        {
            trackObject.Altitude = MaxAltitude;
            Assert.That(_uut.ValidateAltitude(trackObject.Altitude), Is.EqualTo(true));
        }

        [Test]
        public void AltitudeOutsideUpperBoundary_ReturnsFalse()
        {
            trackObject.Altitude = MaxAltitude + 1;
            Assert.That(_uut.ValidateAltitude(trackObject.Altitude), Is.EqualTo(false));
        }

        [Test]
        public void AltitudeInsideLowerBoundary_ReturnsTrue()
        {
            trackObject.Altitude = MinAltitude;
            Assert.That(_uut.ValidateAltitude(trackObject.Altitude), Is.EqualTo(true));
        }

        [Test]
        public void AltitudeOutsideLowerBoundary_ReturnsFalse()
        {
            trackObject.Altitude = MinAltitude - 1;
            Assert.That(_uut.ValidateAltitude(trackObject.Altitude), Is.EqualTo(false));
        }

    }
}

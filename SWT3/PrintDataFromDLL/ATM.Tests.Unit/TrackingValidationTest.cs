using System;
using System.Collections.Generic;
using ATMClasses;
using NUnit.Framework;

namespace ATM.Tests.Unit
{
    [TestFixture]
    public class TrackingValidationTest
    {
        private const int MinCoord = 10000;
        private const int MaxCoord = 90000;
        private const int MinAltitude = 500;
        private const int MaxAltitude = 20000;
        List<string> trackData;
        [SetUp]
        public void Setup()
        {
            trackData = new List<string>{"ATR423", "50000", "50000", "1000", "20151006213456789" };
        }

        //X-Coordinates
        [Test]
        public void XCoordinateInsideUpperBoundary_ReturnsTrue()
        {
            trackData[1] = MaxCoord.ToString();
            Assert.That(TrackingValidation.IsTrackInMonitoredAirspace(trackData), Is.EqualTo(true));
        }

        [Test]
        public void XCoordinateOutsideUpperBoundary_ReturnsFalse()
        {
            trackData[1] = (MaxCoord+1).ToString();
            Assert.That(TrackingValidation.IsTrackInMonitoredAirspace(trackData), Is.EqualTo(false));
        }

        [Test]
        public void XCoordinateInsideLowerBoundary_ReturnsTrue()
        {
            trackData[1] = MinCoord.ToString();
            Assert.That(TrackingValidation.IsTrackInMonitoredAirspace(trackData), Is.EqualTo(true));
        }

        [Test]
        public void XCoordinateOutsideLowerBoundary_ReturnsFalse()
        {
            trackData[1] = (MinCoord-1).ToString();
            Assert.That(TrackingValidation.IsTrackInMonitoredAirspace(trackData), Is.EqualTo(false));
        }


        //Y-Coordinates
        [Test]
        public void YCoordinateInsideUpperBoundary_ReturnsTrue()
        {
            trackData[2] = MaxCoord.ToString();
            Assert.That(TrackingValidation.IsTrackInMonitoredAirspace(trackData), Is.EqualTo(true));
        }

        [Test]
        public void YCoordinateOutsideUpperBoundary_ReturnsFalse()
        {
            trackData[2] = (MaxCoord + 1).ToString();
            Assert.That(TrackingValidation.IsTrackInMonitoredAirspace(trackData), Is.EqualTo(false));
        }

        [Test]
        public void YCoordinateInsideLowerBoundary_ReturnsTrue()
        {
            trackData[2] = MinCoord.ToString();
            Assert.That(TrackingValidation.IsTrackInMonitoredAirspace(trackData), Is.EqualTo(true));
        }

        [Test]
        public void YCoordinateOutsideLowerBoundary_ReturnsFalse()
        {
            trackData[2] = (MinCoord - 1).ToString();
            Assert.That(TrackingValidation.IsTrackInMonitoredAirspace(trackData), Is.EqualTo(false));
        }


        //Altitudes
        [Test]
        public void AltitudeInsideUpperBoundary_ReturnsTrue()
        {
            trackData[3] = MaxAltitude.ToString();
            Assert.That(TrackingValidation.IsTrackInMonitoredAirspace(trackData), Is.EqualTo(true));
        }

        [Test]
        public void AltitudeOutsideUpperBoundary_ReturnsFalse()
        {
            trackData[3] = (MaxAltitude + 1).ToString();
            Assert.That(TrackingValidation.IsTrackInMonitoredAirspace(trackData), Is.EqualTo(false));
        }

        [Test]
        public void AltitudeInsideLowerBoundary_ReturnsTrue()
        {
            trackData[3] = MinAltitude.ToString();
            Assert.That(TrackingValidation.IsTrackInMonitoredAirspace(trackData), Is.EqualTo(true));
        }

        [Test]
        public void 
    }
}

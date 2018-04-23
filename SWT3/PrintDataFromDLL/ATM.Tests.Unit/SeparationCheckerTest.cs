using System;
using System.Collections.Generic;
using ATMClasses;
using ATMClasses.Interfaces;
using NUnit.Framework;

namespace ATM.Tests.Unit
{
    [TestFixture]
    public class SeparationCheckerTest
    {
        TrackObject trackobject1;
        TrackObject trackobject2;
        List<String> list1;
        List<String> list2;

        private IDistance dist;
        ISeparationChecker _uut;

        [SetUp]
        public void Setup()
        {
            dist = new Distance();
            _uut = new SeparationChecker(dist);
            list1 = new List<string> {"MAR123", "50000", "50000", "1000", "20151006213456789"};
            list2 = new List<string> {"MAR123", "50000", "50000", "1000", "20151006213456789"};
            trackobject1 = new TrackObject(list1);
            trackobject2 = new TrackObject(list2);
        }

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
        public void InsideHorizontalAirspaceBigAltitudeDifference_ReturnsTrue(int xCoordTO1, int xCoordTO2, int altitudeTO1,
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
        public void InsideHorizontalAirspaceBigAltitudeDifference_ReturnsFalse(int xCoordTO1, int xCoordTO2, int altitudeTO1,
            int altitudeTO2)
        {
            trackobject1.XCoord = xCoordTO1;
            trackobject1.Altitude = altitudeTO1;
            trackobject2.XCoord = xCoordTO2;
            trackobject2.Altitude = altitudeTO2;

            Assert.That(_uut.IsInOtherAirSpace(trackobject1, trackobject2), Is.EqualTo(false));
        }
    }
}

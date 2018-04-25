using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMClasses;
using ATMClasses.Interfaces;
using NUnit;
using NUnit.Framework;

namespace ATM.Tests.Integration
{
    [TestFixture]
    public class IT1_TrackObject_Distance_TrackUpdater_VelocityCourseCalculator
    {
        IVelocityCourseCalculator _uut;
        IDistance distance;
        TrackObject trackObject1;
        TrackObject trackObject2;
        List<String> list1;
        List<String> list2;

        [SetUp]
        public void Setup()
        {
            list1 = new List<string> { "MAR123", "50000", "50000", "1000", "20151006213456000" };
            list2 = new List<string> { "MAR123", "49900", "49900", "1000", "20151006213457000" };
            trackObject1 = new TrackObject(list1);
            trackObject2 = new TrackObject(list2);
            distance = new Distance();
            _uut = new VelocityCourseCalculater(distance);
        }

        [Test]
        public void CalculateVelocityUsesDistance()
        {
            Assert.AreEqual(141, _uut.CalculateVelocity(trackObject1, trackObject2));
        }

        [Test]
        public void CalculateCourseUsesDistance()
        {
            Assert.AreEqual(225, _uut.CalculateCourse(trackObject1, trackObject2));

        }

    }
}

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
    public class IT2_DateFormatter_Distance_TrackObject_SeperationChecker
    {

        ISeparationChecker _uut;
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
            _uut = new SeparationChecker(distance);

        }

        [Test]
        public void IsInOtherAirspaceUsesDistance()
        {
            Assert.AreEqual(true,_uut.IsInOtherAirSpace(trackObject1, trackObject2));
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMClasses;
using ATMClasses.Interfaces;
using NUnit;
using NUnit.Framework;
using TransponderReceiver;
using NSubstitute;
using NSubstitute.Extensions;


namespace ATM.Tests.Integration
{
    [TestFixture]
    public class IT4_VelocityCalculator_TrackUpdater
    {
        ITrackUpdater _uut;
        IVelocityCourseCalculator velocityCourseCalculator;
        IDistance distance;
        TrackObject trackObject1;
        TrackObject trackObject2;
        List<String> list1;
        List<String> list2;
        List<TrackObject> tList1;
        List<TrackObject> tList2;
        List<TrackObject> returnList;
        [SetUp]
        public void Setup()
        {
            list1 = new List<string> {"MAR123", "50000", "50000", "1000", "20151006213456000"};
            list2 = new List<string> {"MAR123", "49900", "49900", "1000", "20151006213457000"};
            trackObject1 = new TrackObject(list1);
            trackObject2 = new TrackObject(list2);
            tList1 = new List<TrackObject> {trackObject1};
            tList2 = new List<TrackObject>{trackObject2};
            returnList = new List<TrackObject>();
            distance = new Distance();
            velocityCourseCalculator = new VelocityCourseCalculater(distance);
            _uut = new TrackUpdater(velocityCourseCalculator);

        }

        [Test]
        public void TrackUpdaterUsesVelocityCourseCalculator_InCalculateCourse()
        {
            returnList = _uut.updateTracks(tList1, tList2);
            Assert.AreEqual(45, returnList[0].Course);
        }

        [Test]
        public void TrackUpdaterUsesVelocityCourseCalculator_InCalculateVelocity()
        {
            returnList = _uut.updateTracks(tList2, tList1);
            Assert.AreEqual(141, returnList[0].Velocity);
        }
    }
}

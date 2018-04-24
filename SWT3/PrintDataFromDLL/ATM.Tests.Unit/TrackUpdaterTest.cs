using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ATMClasses;
using ATMClasses.Interfaces;
using NSubstitute;

namespace ATM.Tests.Unit
{
    [TestFixture]
    public class TrackUpdaterTest
    {
        
        ITrackUpdater _uut;
        IVelocityCourseCalculator velocityCourseCalculator;
        IDistance distance;
        TrackObject trackObject1;
        TrackObject trackObject2;
        List<string> list1;
        List<string> list2;
        List<TrackObject> newList;
        List<TrackObject> oldList;

        [SetUp]
        public void Setup()
        {
            distance = new Distance();

            velocityCourseCalculator = Substitute.For<IVelocityCourseCalculator>();
            _uut = new TrackUpdater(velocityCourseCalculator);
            
        }

        [Test]
        public void IsCalculateCourseCalled()
        {
            list1 = new List<string> { "MAR123", "50000", "50000", "1000", "20151006213456789" };
            list2 = new List<string> { "MAR123", "50000", "50000", "1000", "20151006213456789" };
            trackObject1 = new TrackObject(list1);
            trackObject2 = new TrackObject(list2);
            newList = new List<TrackObject> {trackObject1};
            oldList = new List<TrackObject> {trackObject2};
            _uut.updateTracks(newList, oldList);

            velocityCourseCalculator.Received().CalculateCourse(trackObject2, trackObject1);
        }

        [Test]
        public void IsCalculateVelocityCalled()
        {
            list1 = new List<string> { "MAR123", "50000", "50000", "1000", "20151006213456789" };
            list2 = new List<string> { "MAR123", "50000", "50000", "1000", "20151006213456789" };
            trackObject1 = new TrackObject(list1);
            trackObject2 = new TrackObject(list2);
            newList = new List<TrackObject> {trackObject1};
            oldList = new List<TrackObject> {trackObject2};
            _uut.updateTracks(newList, oldList);

            velocityCourseCalculator.Received().CalculateVelocity(trackObject2, trackObject1);
        }

       
    }
}

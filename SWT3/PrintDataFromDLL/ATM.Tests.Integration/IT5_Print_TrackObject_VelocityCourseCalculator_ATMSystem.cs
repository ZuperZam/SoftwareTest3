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
    public class IT5_Print_TrackObject_VelocityCourseCalculator_ATMSystem
    {
        ATMSystem _uut;
        ITrackUpdater _trackUpdater;
        IVelocityCourseCalculator _velocityCourseCalculator;
        ISeparationChecker _separationChecker;
        IPrint _print;
        ITransponderReceiver _transponderReceiver;
        ITransponderParsing _transponderParsing;
        ITrackingValidation _trackingValidation;
        IDateFormatter _dateFormatter;
        ITrackListEvent _objectifier;
        IDistance distance;
        List<TrackObject> _trackObjects;
        List<TrackObject> _receivedTrackObjects;
        List<TrackObject> _trackObjects1;
        List<TrackObject> _trackObjects2;
        TrackListEventArgs _trackObjectDataEventArgs;
        
        TrackObject trackObject;

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
            list1 = new List<string> { "MAR123", "50000", "50000", "1000", "20151006213456000" };
            list2 = new List<string> { "MAR123", "49900", "49900", "1000", "20151006213457000" };
            trackObject1 = new TrackObject(list1);
            trackObject2 = new TrackObject(list2);
            tList1 = new List<TrackObject> { trackObject1 };
            tList2 = new List<TrackObject> { trackObject2 };
            returnList = new List<TrackObject>();
            _receivedTrackObjects = new List<TrackObject>();
            trackObject = new TrackObject(list1);
            _trackObjects = new List<TrackObject>();
            _trackObjects.Add(trackObject);
            distance = new Distance();
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _transponderParsing = new TransponderParsing();
            _trackingValidation = new TrackingValidation();
            _dateFormatter = new DateFormatter();
            _velocityCourseCalculator = new VelocityCourseCalculater(distance);
            _trackUpdater = new TrackUpdater(_velocityCourseCalculator);
            _separationChecker = new SeparationChecker(distance);
            _print = Substitute.For<IPrint>();
            _objectifier = Substitute.For<ITrackListEvent>();
            _uut = new ATMSystem(_objectifier, _trackUpdater, _velocityCourseCalculator, _separationChecker, _print);
            _trackObjectDataEventArgs = new TrackListEventArgs(_trackObjects);

            _objectifier.TrackListReady += (sender, TrackListEventArgs) =>
            {
                _receivedTrackObjects = TrackListEventArgs.TrackObjects;
            };
        }

        public void RaiseFakeTracklistEvent()
        {
            _objectifier.TrackListReady += Raise.EventWith(_trackObjectDataEventArgs);    //Raises a fake event with the stated arguments
        }

        [Test]
        public void ATMSystemUsesTrackUpdater_IsCourseCorrect()
        {
            
            returnList =_trackUpdater.updateTracks(tList1, tList2);
            Assert.AreEqual(45, returnList[0].Course);

        }

        [Test]
        public void ATMSystemUsesTrackUpdater_IsVelocityCorrect()
        {
            
            returnList = _trackUpdater.updateTracks(tList1, tList2);
            Assert.AreEqual(45, returnList[0].Velocity);

        }

        [Test]
        public void ATMSystemUsesSeperationChecker_IsInAirSpaceTrueCorrect()
        {
            Assert.AreEqual(true, _separationChecker.IsInOtherAirSpace(trackObject1, trackObject2));
        }

        [Test]
        public void ATMSystemUsesVelocityCourseCalculator_IsCourseCorrect()
        {

            int temp = _velocityCourseCalculator.CalculateCourse(trackObject1, trackObject2);
            Assert.AreEqual(225, temp);
            
        }

        [Test]
        public void ATMSystemUsesVelocityCourseCalculator_IsVelocityCorrect()
        {
            int temp = _velocityCourseCalculator.CalculateVelocity(trackObject1, trackObject2);
            Assert.AreEqual(141, temp);
        }

    }
}

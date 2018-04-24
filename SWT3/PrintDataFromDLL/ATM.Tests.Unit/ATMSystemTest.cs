using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ATMClasses;
using ATMClasses.Interfaces;
using NSubstitute;
using TransponderReceiver;

namespace ATM.Tests.Unit
{
    
    [TestFixture]
    public class ATMSystemTest
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
        List<TrackObject> _trackObjects;
        List<TrackObject> _receivedTrackObjects;
        List<TrackObject> _trackObjects1;
        List<TrackObject> _trackObjects2;
        TrackListEventArgs _trackObjectDataEventArgs;
        List<string> list1;
        TrackObject trackObject;
        [SetUp]
        public void Setup()
        {
            _trackObjects1 = new List<TrackObject>();
            _trackObjects = new List<TrackObject>();
            _trackObjects2 = new List<TrackObject>();
            list1 = new List<string> { "MAR123", "50000", "50000", "1000", "20151006213456789" };
            trackObject = new TrackObject(list1);
            _receivedTrackObjects = new List<TrackObject>();
            _trackObjects.Add(trackObject);
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _transponderParsing = Substitute.For<ITransponderParsing>();
            _trackingValidation = Substitute.For<ITrackingValidation>();
            _dateFormatter = Substitute.For<IDateFormatter>();
            _trackUpdater = Substitute.For<ITrackUpdater>();
            _velocityCourseCalculator = Substitute.For<IVelocityCourseCalculator>();
            _separationChecker = Substitute.For<ISeparationChecker>();
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
        public void IsUpdateTracksCalledCorrectly()
        {
            //_trackUpdater.updateTracks(_receivedTrackObjects, _trackObjects).Returns(_receivedTrackObjects);
            //RaiseFakeTracklistEvent();
        }

        [Test]
        public void Is_isInOtherAirspaceCalledCorrectly()
        {
            //RaiseFakeTransponderReceiverEvent();

            //_uut.OnTrackListReady(, _transponderArgsList[0]);
        }

        [Test]
        public void IsLogSeperationEventCalledCorrectly()
        {
            //RaiseFakeTransponderReceiverEvent();

            //_uut.OnTrackListReady(, _transponderArgsList[0]);
        }
    }
}

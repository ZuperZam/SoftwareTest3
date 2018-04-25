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
    public class IT3_DateFormatter_Distance_TrackUpdater_TrackObject_VelocityCourseCalculator
    {
        private Objectifier _uut;
        private ITransponderReceiver _transponderReceiver;
        private ITransponderParsing _transponderParsing;
        private ITrackingValidation _trackingValidation;
        private IDateFormatter _dateFormatter;
        private List<string> _transponderArgsList;
        private RawTransponderDataEventArgs _transponderDataEventArgs;

        [SetUp]
        public void Setup()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _transponderParsing = new TransponderParsing();
            _trackingValidation = new TrackingValidation();
            _dateFormatter = new DateFormatter();
            _uut = new Objectifier(_transponderReceiver, _trackingValidation, _transponderParsing, _dateFormatter);

            _transponderArgsList = new List<string> { "ATR423;39045;12932;14000;20151006213456789" };
            _transponderDataEventArgs = new RawTransponderDataEventArgs(_transponderArgsList);
        }



        public void RaiseFakeTransponderReceiverEvent()
        {
            _transponderReceiver.TransponderDataReady += Raise.EventWith(_transponderDataEventArgs);    //Raises a fake event with the stated arguments
        }

        [Test]
        public void ObjectifierUsesTransponderParsing()
        {
            List<string> expectedStringList = new List<string>() { "ATR423", "39045", "12932", "14000", "20151006213456789" };
            Assert.That(_transponderParsing.TransponderParser(_transponderArgsList[0]), Is.EqualTo(expectedStringList));
        }

        [Test]
        public void ObjectifierUsesTrackingValidation()
        {
            List<string> expectedStringList = new List<string>() { "ATR423", "39045", "12932", "14000", "20151006213456789" };
            Assert.That(_trackingValidation.IsTrackInMonitoredAirspace(expectedStringList), Is.True);
        }

        [Test]
        public void ObjectifierUsesDateFormatter()
        {
            List<string> expectedStringList = new List<string>() { "ATR423", "39045", "12932", "14000", "20151006213456789" };
            Assert.That(_dateFormatter.FormatTimestamp(expectedStringList[4]), Is.EqualTo("October 6th, 2015, at 21:34:56 and 789 milliseconds"));

        }
    }
}

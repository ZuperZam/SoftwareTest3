using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ATMClasses;
using ATMClasses.Interfaces;
using NSubstitute;
using NSubstitute.Extensions;
using TransponderReceiver;

namespace ATM.Tests.Unit
{
    [TestFixture]
    public class ObjectifierTest
    {
        private Objectifier _uut;
        private ITransponderReceiver _transponderReceiver;
        private ITransponderParsing _transponderParsing;
        private ITrackingValidation _trackingValidation;
        private IDateFormatter _dateFormatter;

        private List<TrackObject> _trackObjects = new List<TrackObject>();
        private List<string> _transponderArgsList;
        private RawTransponderDataEventArgs _transponderDataEventArgs;

        [SetUp]
        public void Setup()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _transponderParsing = Substitute.For<ITransponderParsing>();
            _trackingValidation = Substitute.For<ITrackingValidation>();
            _dateFormatter = Substitute.For<IDateFormatter>();

            _uut = new Objectifier(_transponderReceiver, _trackingValidation, _transponderParsing, _dateFormatter);

            _transponderArgsList = new List<string> { "ATR423;39045;12932;14000;20151006213456789" };
            _transponderDataEventArgs = new RawTransponderDataEventArgs(_transponderArgsList);

            _uut.TrackListReady += (sender, TrackListEventArgs) =>
            {  
                _trackObjects = TrackListEventArgs.TrackObjects;
            };
        }

        public void RaiseFakeTransponderReceiverEvent()
        {
            _transponderReceiver.TransponderDataReady += Raise.EventWith(_transponderDataEventArgs);    //Raises a fake event with the stated arguments
        }

        //When all other classes are faked, the Tracks won't be objectified- Looking at _trackObjects.Count is therefore not possible
        [Test]
        public void Objectifier_OneTrackInList_ReceivedCorrectly()
        {
            RaiseFakeTransponderReceiverEvent();
            _transponderParsing.Received().TransponderParser((_transponderArgsList[0]));
        }

        [Test]
        public void Objectifier_TwoTracksInList_ReceivedCorrectly()
        {
            _transponderArgsList.Add("TRI423;39045;12932;14000;20151006213456789");

            RaiseFakeTransponderReceiverEvent();
            _transponderParsing.Received().TransponderParser((_transponderArgsList[1]));
        }
    }
}

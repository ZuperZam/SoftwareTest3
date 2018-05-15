using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMRefactored;
using ATMRefactored.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TransponderReceiver;

namespace ATMRefactored.Tests.Integration
{
    [TestFixture]
    public class IT1_TransponderParsing_TrackObject
    {

        TransponderParsing _uut;
        TrackObject trackObject;

        ITrackingFiltering trackingFiltering;
        ITrackUpdater trackUpdater;
        ITransponderReceiver receiver;

        List<string> list;
        private List<string> _transponderArgsList;
        List<TrackObject> trackObjectList;
        private RawTransponderDataEventArgs _transponderDataEventArgs;

        private string Tag = "ATR423";
        private string X = "39045";
        private string Y = "12932";
        private string Alt = "14000";
        private string Timestamp = "20151006213456789";
        [SetUp]
        public void Setup()
        {
            receiver = Substitute.For<ITransponderReceiver>();
            trackUpdater = Substitute.For<ITrackUpdater>();
            trackingFiltering = new TrackingFiltering(trackUpdater);
            _uut = new TransponderParsing(receiver, trackingFiltering);
            _transponderArgsList = new List<string> { "ATR423;39045;12932;14000;20151006213456789" };
            _transponderDataEventArgs = new RawTransponderDataEventArgs(_transponderArgsList);
            trackObjectList = new List<TrackObject>();
            list = new List<string> {Tag, X, Y, Alt, Timestamp};
            trackObject =
                new TrackObject(list) {PrettyTimeStamp = "October 6th, 2015, at 21:34:56 and 789 milliseconds"};
            trackObjectList.Add(trackObject);
            
        }

        public void RaiseFakeTransponderReceiverEvent()
        {
            receiver.TransponderDataReady += Raise.EventWith(_transponderDataEventArgs);    //Raises a fake event with the stated arguments
        }

        [Test]
        public void TransponderParsing_uses_TrackingFiltering()
        {
            RaiseFakeTransponderReceiverEvent();

            trackUpdater.Received().UpdateTracks(trackObjectList);
        }
    }
}

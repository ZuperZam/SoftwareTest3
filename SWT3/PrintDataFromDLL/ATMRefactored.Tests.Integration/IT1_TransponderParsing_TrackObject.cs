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
        private List<string> _transponderArgsList_Success;
        private List<string> _transponderArgsList_Failure;
        List<TrackObject> trackObjectList;
        private RawTransponderDataEventArgs _transponderDataEventArgs_Success;
        private RawTransponderDataEventArgs _transponderDataEventArgs_Failure;


        //private string Tag = "ATR423";
        //private string X = "39045";
        //private string Y = "12932";
        //private string Alt = "14000";
        //private string Timestamp = "20151006213456789";
        [SetUp]
        public void Setup()
        {
            receiver = Substitute.For<ITransponderReceiver>();
            trackUpdater = Substitute.For<ITrackUpdater>();
            trackingFiltering = new TrackingFiltering(trackUpdater);
            _uut = new TransponderParsing(receiver, trackingFiltering);
            _transponderArgsList_Success = new List<string> { "ATR423;39045;12932;14000;20151006213456789" };
            _transponderArgsList_Failure = new List<string> { "ATR423;328085;12932;14000;20151006213456789" };
            _transponderDataEventArgs_Success = new RawTransponderDataEventArgs(_transponderArgsList_Success);
            _transponderDataEventArgs_Failure = new RawTransponderDataEventArgs(_transponderArgsList_Success);
            //trackObjectList = new List<TrackObject>();
            //list = new List<string> {Tag, X, Y, Alt, Timestamp};
            //trackObject =
            //    new TrackObject(list) {PrettyTimeStamp = "October 6th, 2015, at 21:34:56 and 789 milliseconds"};
            //trackObjectList.Add(trackObject);
            
        }

        public void RaiseFakeTransponderReceiverEvent_Success()
        {
            receiver.TransponderDataReady += Raise.EventWith(_transponderDataEventArgs_Success);    //Raises a fake event with the stated arguments
        }

        [Test]
        public void TransponderParsing_uses_TrackingFiltering_Success()
        {
            RaiseFakeTransponderReceiverEvent_Success();

            trackUpdater.Received().UpdateTracks(Arg.Is<List<TrackObject>>(data => data[0].Tag == "ATR423" && data[0].XCoord == 39045 && data[0].YCoord == 12932 && data[0].Altitude == 14000 && data[0].PrettyTimeStamp == "October 6th, 2015, at 21:34:56 and 789 milliseconds"));
        }

        public void RaiseFakeTransponderReceiverEvent_Failure()
        {
            receiver.TransponderDataReady += Raise.EventWith(_transponderDataEventArgs_Failure);    //Raises a fake event with the stated arguments
        }

        [Test]
        public void TransponderParsing_uses_TrackingFiltering_Failure()
        {
            RaiseFakeTransponderReceiverEvent_Failure();

            trackUpdater.DidNotReceive().UpdateTracks(Arg.Is<List<TrackObject>>(data => data[0].Tag == "ATR423" && data[0].XCoord == 328085 && data[0].YCoord == 12932 && data[0].Altitude == 14000 && data[0].PrettyTimeStamp == "October 6th, 2015, at 21:34:56 and 789 milliseconds"));
        }
    }
}

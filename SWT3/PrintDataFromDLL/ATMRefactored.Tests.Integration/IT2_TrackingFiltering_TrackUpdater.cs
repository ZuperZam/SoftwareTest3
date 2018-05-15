using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMRefactored.Interfaces;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace ATMRefactored.Tests.Integration
{
    class IT2_TrackingFiltering_TrackUpdater
    {

        TransponderParsing _sut;
        TrackObject trackObject;

        ITrackingFiltering trackingFiltering;
        ITrackUpdater trackUpdater;
        ITransponderReceiver receiver;
        ISeperationEvent seperationEvent;
        ITrackRendition trackRendition;

        List<string> list;
        private List<string> _transponderArgsList_Success;
        private List<string> _transponderArgsList_SecondEvent_Success;
        List<TrackObject> trackObjectList;
        private RawTransponderDataEventArgs _transponderDataEventArgs_Success;
        private RawTransponderDataEventArgs _transponderDataEventArgs_SecondEvent_Success;

        [SetUp]
        public void Setup()
        {
            receiver = Substitute.For<ITransponderReceiver>();
            seperationEvent = Substitute.For<ISeperationEvent>();
            trackRendition = Substitute.For<ITrackRendition>();
            trackUpdater = new TrackUpdater(seperationEvent, trackRendition);
            trackingFiltering = new TrackingFiltering(trackUpdater);
            _sut = new TransponderParsing(receiver, trackingFiltering);
            _transponderArgsList_Success = new List<string> { "ATR423;39045;12932;14000;20151006213456789" };
            _transponderArgsList_SecondEvent_Success = new List<string> { "ATR423;39045;12934;14000;20151006213457789" };
            _transponderDataEventArgs_Success = new RawTransponderDataEventArgs(_transponderArgsList_Success);
            _transponderDataEventArgs_SecondEvent_Success = new RawTransponderDataEventArgs(_transponderArgsList_SecondEvent_Success);

        }

        public void RaiseFakeTransponderReceiverEvent_Success()
        {
            receiver.TransponderDataReady += Raise.EventWith(_transponderDataEventArgs_Success);    //Raises a fake event with the stated arguments
        }

        public void RaiseFakeTransponderReceiverEvent_SecondEvent_Success()
        {
            receiver.TransponderDataReady += Raise.EventWith(_transponderDataEventArgs_SecondEvent_Success);    //Raises a fake event with the stated arguments
        }

        [Test]
        public void NoPreviousElement_In_TrackUpdater_SeperationEvent()
        {
            RaiseFakeTransponderReceiverEvent_Success();

            seperationEvent.Received().CheckEvents(Arg.Is<List<TrackObject>>(data => data[0].Course == 0 && data[0].Velocity == 0));
        }

        [Test]
        public void PreviousElement_In_TrackUpdater_SeperationEvent()
        {
            RaiseFakeTransponderReceiverEvent_Success();
            RaiseFakeTransponderReceiverEvent_SecondEvent_Success();

            seperationEvent.Received().CheckEvents(Arg.Is<List<TrackObject>>(data => data[0].Course == 0 && data[0].Velocity == 2));
        }

        [Test]
        public void NoPreviousElement_In_TrackUpdater_TrackRendition()
        {
            RaiseFakeTransponderReceiverEvent_Success();

            trackRendition.Received().RenderTrack(Arg.Is<List<TrackObject>>(data => data[0].Course == 0 && data[0].Velocity == 0));
        }

        [Test]
        public void PreviousElement_In_TrackUpdater_TrackRendition()
        {
            RaiseFakeTransponderReceiverEvent_Success();
            RaiseFakeTransponderReceiverEvent_SecondEvent_Success();

            trackRendition.Received().RenderTrack(Arg.Is<List<TrackObject>>(data => data[0].Course == 0 && data[0].Velocity == 2));
        }

    }
}

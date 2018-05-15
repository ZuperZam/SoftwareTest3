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
    class IT3_TrackUpdater_SeperationEvent_TrackRendition
    {
        TransponderParsing _sut;
        TrackObject trackObject;

        ITrackingFiltering trackingFiltering;
        ITrackUpdater trackUpdater;
        ITransponderReceiver receiver;
        ISeperationEvent seperationEvent;
        ITrackRendition trackRendition;
        IEventRendition eventRendition;
        ILogWriter logWriter;

        List<string> list;
        private List<string> _transponderArgsList_Separation;
        private List<string> _transponderArgsList_No_Separation;
        List<TrackObject> trackObjectList;
        private RawTransponderDataEventArgs _transponderDataEventArgs_Separation;
        private RawTransponderDataEventArgs _transponderDataEventArgs_No_Separation;

        [SetUp]
        public void Setup()
        {
            receiver = Substitute.For<ITransponderReceiver>();
            eventRendition = Substitute.For<IEventRendition>();
            logWriter = Substitute.For<ILogWriter>();
            seperationEvent = new SeperationEvent(logWriter, eventRendition);
            trackRendition = new TrackRendition();
            trackUpdater = new TrackUpdater(seperationEvent, trackRendition);
            trackingFiltering = new TrackingFiltering(trackUpdater);
            _sut = new TransponderParsing(receiver, trackingFiltering);
            _transponderArgsList_Separation = new List<string> { "FAT423;39045;12932;14000;20151006213456789", "MAR423;39045;12932;14000;20151006213456789" };
            _transponderArgsList_No_Separation = new List<string> { "MAR423;39045;12932;12000;20151006213456789", "FAT423;39045;12932;14000;20151006213456789" };
            _transponderDataEventArgs_Separation = new RawTransponderDataEventArgs(_transponderArgsList_Separation);
            _transponderDataEventArgs_No_Separation = new RawTransponderDataEventArgs(_transponderArgsList_No_Separation);

        }

        public void RaiseFakeTransponderReceiverEvent_Separation()
        {
            receiver.TransponderDataReady += Raise.EventWith(_transponderDataEventArgs_Separation);    //Raises a fake event with the stated arguments
        }

        public void RaiseFakeTransponderReceiverEvent_No_Separation()
        {
            receiver.TransponderDataReady += Raise.EventWith(_transponderDataEventArgs_No_Separation);    //Raises a fake event with the stated arguments
        }

        [Test]
        public void No_Separation_EventRendition()
        {
            RaiseFakeTransponderReceiverEvent_No_Separation();
            eventRendition.DidNotReceive().RenderEvent("FAT423 and MAR423 are breaking separation rules");
        }

        [Test]
        public void Separation_EventRendition()
        {
            RaiseFakeTransponderReceiverEvent_Separation();
            eventRendition.Received().RenderEvent("FAT423 and MAR423 are breaking separation rules");
        }

        [Test]
        public void No_Separation_LogWriter()
        {
            RaiseFakeTransponderReceiverEvent_No_Separation();
            logWriter.DidNotReceive().LogEvent("Timestamp: 06-10-2015 21:34:56	FAT423 and MAR423 are breaking separation rules");
        }

        [Test]
        public void Separation_LogWriter()
        {
            RaiseFakeTransponderReceiverEvent_Separation();
            logWriter.Received().LogEvent("Timestamp: 06-10-2015 21:34:56	FAT423 and MAR423 are breaking separation rules");
        }
    }
}

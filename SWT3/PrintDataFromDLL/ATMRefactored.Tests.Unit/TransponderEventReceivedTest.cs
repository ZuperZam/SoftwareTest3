using System.Collections.Generic;
using ATMRefactored;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace ATMRefactored.Tests.Unit
{
    [TestFixture]
    class TransponderDataEventReceived
    {
        private ITransponderReceiver _transponderReceiver;
        private List<string> _argList;

        [SetUp]
        public void Setup()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _argList = new List<string> {"ATR423;39045;12932;14000;20151006213456789"}; //Sets up eventArg

        }

        //Testing that the expected EventArg is received when an event is raised
        [Test]
        public void TransponderDataReady_EventFired_EventReceived() 
        {
            var args = new RawTransponderDataEventArgs(_argList);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);

            Assert.That(args.TransponderData, Is.EqualTo(_argList));

        }


        //Testing that the EventArg is not the same as a wrong EventArg
        [Test]
        public void TransponderDataReady_EventFired_EventDataNotEqualToTest()
        {
            List<string> wrongArg = new List<string> {"ATR423;39045;12932;15000;20151006213456789"};
            var args = new RawTransponderDataEventArgs(_argList);

            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);

            Assert.AreNotEqual(args.TransponderData, wrongArg);

        }

    }
}

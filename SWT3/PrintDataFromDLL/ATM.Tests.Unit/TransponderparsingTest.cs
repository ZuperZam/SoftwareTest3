using System.Collections.Generic;
using ATMClasses;
using NUnit.Framework;
using ATMClasses.Interfaces;
namespace ATM.Tests.Unit
{
    [TestFixture]
    class TransponderparsingTest
    {
        private List<string> testList = new List<string>();
        private string Tag = "ATR423";
        private string X = "39045";
        private string Y = "12932";
        private string Alt = "14000";
        private string Timestamp = "20151006213456789";
        ITransponderParsing _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new TransponderParsing();
            testList.Add(Tag);
            testList.Add(X);
            testList.Add(Y);
            testList.Add(Alt);
            testList.Add(Timestamp);
            
        }

        [Test]
        public void AreListsIdentical()
        {
            string test = "ATR423;39045;12932;14000;20151006213456789";

            
            Assert.That(_uut.TransponderParser(test), Is.EqualTo(testList));
        }

        [Test]
        public void AreNotEqualList()
        {
            string test = "MAR123;39045;12932;14000;20151006213456789";

            Assert.AreNotEqual(_uut.TransponderParser(test), (testList));
        }

      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using ATMClasses;
using NUnit.Framework;

namespace ATM.Tests.Unit
{
    class TransponderparsingTest
    {
        private List<string> testList = new List<string>();
        private string Tag = "ATR423";
        private string X = "39045";
        private string Y = "12932";
        private string Alt = "14000";
        private string Timestamp = "20151006213456789";
    

        [SetUp]
        public void Setup()
        {
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

            
            Assert.That(TransponderParsing.TransponderParser(test), Is.EqualTo(testList));
        }

        [Test]
        public void AreList()
        {
            string test = "MAR123;39045;12932;14000;20151006213456789";

            Assert.AreNotEqual(TransponderParsing.TransponderParser(test), (testList));
        }

        [Test]
        public void IsYCorrect()
        {
            
        }

        [Test]
        public void IsAltitudeCorrect()
        {
            
        }

        [Test]
        public void IsTimestampCorrect()
        {
            
        }
    }
}

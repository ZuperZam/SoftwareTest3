using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ATM.Tests.Unit
{
    class TransponderparsingTest
    {
        private List<string> testList = new List<string>();
        private string Tag = "ATR423";
        private string X = "39045";
        private string y = "12932";
        private string Alt = "14000";
        private string Timestamp = "20151006213456789";
    

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void IsTagCorrect()
        {
            string test = "ATR423;39045;12932;14000;20151006213456789";
            testList = test.Split(';').ToList();
            Assert.That(testList[0], Is.EqualTo(Tag));
        }
    }
}

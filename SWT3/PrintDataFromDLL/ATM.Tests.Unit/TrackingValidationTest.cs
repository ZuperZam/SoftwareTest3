using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ATM.Tests.Unit
{
    [TestFixture]
    public class TrackingValidationTest
    {
        [SetUp]
        public void Setup()
        {
            var trackData = new List<string>();

            trackData.Add("ATR423");
            trackData.Add("39045");
            trackData.Add("12932");
            trackData.Add("14000");
            trackData.Add("20151006213456789");
        }

        
    }
}

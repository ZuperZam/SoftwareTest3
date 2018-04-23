using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ATMClasses.Interfaces;
using ATMClasses;

namespace ATM.Tests.Unit
{
    [TestFixture]
    public class VelocityCourseCalculatorTest
    {
        IVelocityCourseCalculator _uut;
        IDistance distance;
        TrackObject trackobject1;
        TrackObject trackobject2;
        List<String> list1;
        List<String> list2;

        [SetUp]
        public void Setup()
        {
            distance = new Distance();
            _uut = new VelocityCourseCalculater(distance);
            list1 = new List<string> { "MAR123", "50000", "50000", "1000", "20151006213456789" };
            list2 = new List<string> { "MAR123", "50000", "50000", "1000", "20151006213456789" };
            trackobject1 = new TrackObject(list1);
            trackobject2 = new TrackObject(list2);
        }

        
        [TestCase(5000, 5000, 5000, 5100, 0)]
        [TestCase(5000, 5002, 5000, 5100, 1)]
        [TestCase(5000, 5000, 5000, 5000, 90)]
        [TestCase(5000, 5100, 5000, 5100, 45)]
        [TestCase(5000, 5100, 5000, 5000, 90)]
        [TestCase(5000, 5100, 5100, 5000, 135)]
        [TestCase(5000, 5000, 5100, 5000, 180)]
        [TestCase(5100, 5000, 5100, 5000, 225)]
        [TestCase(5100, 5000, 5000, 5000, 270)]
        [TestCase(5100, 5000, 5000, 5100, 315)]
        [TestCase(5000, 4999, 5000, 5100, 359)]
        public void IsCourseCorrect(int x1, int x2, int y1, int y2, int result)
        {
            trackobject1.XCoord = x1;
            trackobject1.YCoord = y1;
            trackobject2.XCoord = x2;
            trackobject2.YCoord = y2;

            Assert.That(_uut.CalculateCourse(trackobject1, trackobject2), Is.EqualTo(result));
        }

        [TestCase(5000, 5000, 5000, 5100, "20151006213456000", "20151006213457000", 100)]
        [TestCase(5000, 5000, 5000, 5100, "20151006213456000", "20151006213456500", 200)]
        [TestCase(5000, 5000, 5000, 5100, "20151006213456000", "20151006213456001", 100000)]
        [TestCase(5000, 5000, 5000, 5100, "20151006213456000", "20151006213456001", 100000)]
        [TestCase(5000, 5100, 5000, 5100, "20151006213456000", "20151006213457000", 141)]
        [TestCase(5000, 5100, 5000, 5100, "20151006213456000", "20151006213456500", 282)]
        [TestCase(5100, 5000, 5100, 5000, "20151006213456000", "20151006213457000", 141)]
        [TestCase(5100, 5000, 5100, 5000, "20151006213456000", "20151006213456500", 282)]
        public void IsVelocityCorrect(int x1, int x2, int y1, int y2, string timestamp1, string timestamp2, int result)
        {
            trackobject1.XCoord = x1;
            trackobject1.YCoord = y1;
            trackobject2.XCoord = x2;
            trackobject2.YCoord = y2;

            trackobject1.Timestamp = DateTime.ParseExact(timestamp1, "yyyyMMddHHmmssfff",
                System.Globalization.CultureInfo.InvariantCulture);
            trackobject2.Timestamp = DateTime.ParseExact(timestamp2, "yyyyMMddHHmmssfff",
                System.Globalization.CultureInfo.InvariantCulture);

            Assert.That(_uut.CalculateVelocity(trackobject1, trackobject2), Is.EqualTo(result));
        }
    }
}

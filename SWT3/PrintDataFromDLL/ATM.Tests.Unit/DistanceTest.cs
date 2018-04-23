using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ATMClasses;

namespace ATM.Tests.Unit
{
    

    [TestFixture]
    public class DistanceTest
    {
        IDistance _uut;
        int x1, x2, y1, y2;
        [SetUp]
        public void Setup()
        {
            _uut = new Distance();

        }

        [TestCase(5, 10, 5)]
        [TestCase(10, 5, 5)]
        [TestCase(-5, 10, 5)]
        [TestCase(5, -10, 15)]
        
        public void Is1DDistanceCorrect(int x1, int x2, int result)
        {
            result = Math.Abs(x1 - x2);
            Assert.That(_uut.CalculateDistance1D(x1, x2), Is.EqualTo(result));
        }

        [TestCase(5, 10, 5, 10, 7)]
        [TestCase(10, 5, 10, 5, 7)]
        [TestCase(-5, 10, 5, -10, 450)]
        [TestCase(5, -10, -5, 10, -450)]
        public void Is2DDistanceCorrect(int x1, int x2, int y1, int y2, double result)
        {
            Int64 xDist = _uut.CalculateDistance1D(x1, x2);
            Int64 yDist = _uut.CalculateDistance1D(y1, y2);
            result = Math.Sqrt((xDist * xDist) + (yDist * yDist));
            Assert.That(_uut.CalculateDistance2D(x1, x2, y1, y2), Is.EqualTo(result));
        }
    }
}

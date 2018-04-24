using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ATMClasses;
using ATMClasses.Interfaces;

namespace ATM.Tests.Unit
{
    [TestFixture]
    public class TrackUpdaterTest
    {
        
        ITrackUpdater = _uut;
        IVelocityCourseCalculator velocityCourseCalculator;
        private IDistance distance;

        [SetUp]
        public void Setup()
        {
            distance = new Distance();
            velocityCourseCalculator = new VelocityCourseCalculater(distance);

        }
    }
}

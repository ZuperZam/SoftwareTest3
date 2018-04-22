using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMClasses
{
    class VelocityCourseCalculater
    {
        private IDistance dist;

        public VelocityCourseCalculater(IDistance distance)
        {
            dist = distance;
        }

        //Returns course in whole degrees. North is 0
        public int CalculateCourse(TrackObject oldTO, TrackObject newTO)
        {
            // angle in degrees
            var angleDeg = Math.Atan2(-(newTO.YCoord - oldTO.YCoord), newTO.XCoord - oldTO.XCoord) * 180 / Math.PI;

            angleDeg += 90;

            if (angleDeg < 0)
                angleDeg += 360;

            return (int)angleDeg;
        }

        //Returns velocity in whole meters per second
        public int CalculateVelocity(TrackObject oldTO, TrackObject newTO)
        {
            TimeSpan timeDiff = newTO.Timestamp - oldTO.Timestamp;
            double dist = this.dist.CalculateDistance2D(oldTO.XCoord, newTO.XCoord, oldTO.YCoord, newTO.YCoord);

            return (int)(dist / (timeDiff.Milliseconds * 1000));    //This will give dist m / timeDiff s
        }
    }
}

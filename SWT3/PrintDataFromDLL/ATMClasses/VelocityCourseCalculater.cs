using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMClasses
{
    class VelocityCourseCalculater
    {
        public int CalculateCourse(TrackObject oldTO, TrackObject newTO)
        {
            // angle in degrees
            var angleDeg = Math.Atan2(-(newTO.YCoord - oldTO.YCoord), newTO.XCoord - oldTO.XCoord) * 180 / Math.PI;

            angleDeg += 90;

            if (angleDeg < 0)
                angleDeg += 360;

            return (int)angleDeg;
        }

        public int CalculateVelocity(TrackObject oldTO, TrackObject newTO)
        {
            TimeSpan timeDiff = newTO.Timestamp - oldTO.Timestamp;
            double dist = CalculateDistance2D(oldTO.XCoord, newTO.XCoord, oldTO.YCoord, newTO.YCoord);

            return (int)(dist / (timeDiff.Milliseconds * 1000));
        }

        private int CalculateDistance1D(int x1, int x2)
        {
            return Math.Abs(x1 - x2);
        }

        private double CalculateDistance2D(int x1, int x2, int y1, int y2)
        {
            Int64 xDist = CalculateDistance1D(x1, x2);
            Int64 yDist = CalculateDistance1D(y1, y2);

            return Math.Sqrt((xDist*xDist) + (yDist*yDist));
        }
    }
}

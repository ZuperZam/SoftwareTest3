using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Math;

namespace ATMClasses
{
    class CalculateCourse
    {
        public CalculateCourse(List<TrackObject> current, List<TrackObject> previous)
        {
            foreach (var curr in current)
            {
                foreach (var prev in previous)
                {
                    if (curr.Tag == prev.Tag)
                    {
                        curr.Course = (Math.Atan2((curr.YCoord - prev.YCoord), (curr.XCoord - prev.XCoord))) * (180 / Math.PI);
                        break;
                    }
                }
            }
        }
    }
}

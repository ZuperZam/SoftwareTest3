using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMClasses
{
    public class CalculateVelocity
    {
        public CalculateVelocity(List<TrackObject> current, List<TrackObject> previous)
        {
            foreach(var curr in current)
            {
                foreach(var prev in previous)
                {
                    if (curr.Tag == prev.Tag)
                    {
                        curr.Velocity = ((curr.XCoord - prev.XCoord) ^ 2 + (curr.YCoord - prev.YCoord) ^ 2) / (curr.Timestamp.Subtract(prev.Timestamp));
                        break;
                    }
                }
            }
        }
    }
}

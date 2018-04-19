﻿using System;
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
                        return ((curr.XCoordinate - prev.XCoordinate) ^ 2 + (curr.YCoordinate - prev.YCoordinate) ^ 2) / (curr.Timestamp.Subtract(prev.Timestamp));
                    }
                }
            }
        }
    }
}

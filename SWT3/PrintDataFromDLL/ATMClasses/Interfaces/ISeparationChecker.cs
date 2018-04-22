using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMClasses
{
    interface ISeparationChecker
    {
        bool IsInOtherAirSpace(TrackObject TO1, TrackObject TO2);
    }
}

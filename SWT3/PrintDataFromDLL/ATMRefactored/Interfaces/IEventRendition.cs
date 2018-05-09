using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMRefactored.Interfaces
{
    public interface IEventRendition
    {
        void RenderEvents(List<TrackObject> objectsToCheck);
        bool IsInOtherAirSpace(TrackObject TO1, TrackObject TO2);

        void LogSeparationEvent(TrackObject TO1, TrackObject TO2);

        int CalculateDistance1D(int x1, int x2);

        double CalculateDistance2D(int x1, int x2, int y1, int y2);
    }
}

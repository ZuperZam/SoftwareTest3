using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMRefactored.Interfaces
{
    public interface ITrackUpdater
    {
           void UpdateTracks(List<TrackObject> newTrackObjects);

           int CalculateCourse(TrackObject oldTO, TrackObject newTO);

           int CalculateVelocity(TrackObject oldTO, TrackObject newTO);

           int CalculateDistance1D(int x1, int x2);

           List<TrackObject> _oldTrackObjects { get; set; }

           List<TrackObject> _trackObjects { get; set; }
        double CalculateDistance2D(int x1, int x2, int y1, int y2);

    }
}

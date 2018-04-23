using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMClasses.Interfaces
{
    public class TrackListEventArgs : EventArgs
    {
        public List<TrackObject> TrackObjects { get; }
        public TrackListEventArgs(List<TrackObject> trackObjects)
        {
            TrackObjects = trackObjects;
        }
    }

    public interface ITrackListEvent
    {
        event EventHandler<TrackListEventArgs> TrackListReady;
    }
}

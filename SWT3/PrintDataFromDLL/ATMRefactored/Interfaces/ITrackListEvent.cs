using System;
using System.Collections.Generic;

namespace ATMRefactored.Interfaces
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

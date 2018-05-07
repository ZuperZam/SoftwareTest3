﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMRefactored.Interfaces
{
    public interface ITrackUpdater
    {
           void updateTracks(List<TrackObject> newTrackObjects, List<TrackObject> oldTrackObjects);
    }
}

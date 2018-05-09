using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMRefactored.Interfaces
{
    public interface ITrackingFiltering
    {
        List<TrackObject> filteredTrackObjects { get; set; }
        void IsTrackInMonitoredAirspace(List<TrackObject> trackToCheck);

        bool ValidateCoordinate(int xCoordinate, int yCoordinate);

        bool ValidateAltitude(int altitude);
    }
}

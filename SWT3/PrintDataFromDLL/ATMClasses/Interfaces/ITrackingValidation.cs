using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMClasses.Interfaces
{
    public interface ITrackingValidation
    {
        bool IsTrackInMonitoredAirspace(List<string> trackToCheck);
        bool ValidateCoordinate(string xCoordinate, string yCoordinate);

        bool ValidateAltitude(string altitude);
    }
}

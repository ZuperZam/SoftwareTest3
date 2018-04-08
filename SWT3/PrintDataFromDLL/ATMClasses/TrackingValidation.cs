using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMClasses
{
    public class TrackingValidation
    {
        private const int MinCoordinates = 10000;
        private const int MaxCoordinates = 90000;
        private const int MinAltitude = 500;
        private const int MaxAltitude = 20000;
        public static bool IsTrackInMonitoredAirspace(List<string> trackToCheck)
        {
            //Checks if X [1] and Y [2] coordinates are in the monitored area
            //And if altitude [3] is in monitored area
            return (ValidateCoordinate(trackToCheck[1], trackToCheck[2]) &&
                    ValidateAltitude(trackToCheck[3]));
        }

        private static bool ValidateCoordinate(string xCoordinate, string yCoordinate)
        {
            return (MinCoordinates <= int.Parse(xCoordinate) &&
                    MinCoordinates <= int.Parse(yCoordinate) &&
                    int.Parse(xCoordinate) <= MaxCoordinates &&
                    int.Parse(yCoordinate) <= MaxCoordinates);
        }

        private static bool ValidateAltitude(string altitude)
        {
            return (MinAltitude <= int.Parse(altitude) &&
                    int.Parse(altitude) <= MaxAltitude);
        }
    }
}

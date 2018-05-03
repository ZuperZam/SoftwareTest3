﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMRefactored
{
    public class TrackingFiltering
    {
        private const int MinCoordinates = 10000;
        private const int MaxCoordinates = 90000;
        private const int MinAltitude = 500;
        private const int MaxAltitude = 20000;
        public void IsTrackInMonitoredAirspace(List<TrackObject> trackToCheck)
        {
            //Checks if X [1] and Y [2] coordinates are in the monitored area
            //And if altitude [3] is in monitored area
            foreach (var data in trackToCheck)
            {
                if (!ValidateCoordinate(data.XCoord, data.YCoord) && !ValidateAltitude(data.Altitude))
                {
                    trackToCheck.Remove(data);
                }
                
            }

            trackToCheck.TrimExcess();



        }

        public bool ValidateCoordinate(int xCoordinate, int yCoordinate)
        {
            return (MinCoordinates <= (xCoordinate) &&
                    MinCoordinates <= (yCoordinate) &&
                    (xCoordinate) <= MaxCoordinates &&
                    (yCoordinate) <= MaxCoordinates);
        }

        public bool ValidateAltitude(int altitude)
        {
            return (MinAltitude <= (altitude) &&
                    (altitude) <= MaxAltitude);
        }

    }
}
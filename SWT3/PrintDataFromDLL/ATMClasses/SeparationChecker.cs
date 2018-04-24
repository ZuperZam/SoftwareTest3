using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMClasses
{
    public class SeparationChecker : ISeparationChecker
    {
        private IDistance dist;
        private int verticalSeparation = 300;
        private int horizontalSeparation = 5000;
        public SeparationChecker(IDistance distance)
        {
            dist = distance;
        }

        //Will return true if a Track is in another Track's airspace
        public bool IsInOtherAirSpace(TrackObject TO1, TrackObject TO2)
        {
            double horizontalDistance = dist.CalculateDistance2D(TO1.XCoord, TO2.XCoord, TO1.YCoord, TO2.YCoord);
            int verticalDistance = dist.CalculateDistance1D(TO1.Altitude, TO2.Altitude);

            return horizontalDistance < horizontalSeparation && verticalDistance < verticalSeparation;
        }

        public void LogSeparationEvent(TrackObject TO1, TrackObject TO2)
        {
            string output = "Timestamp: " + TO1.Timestamp + "\t" +
                            TO1.Tag + " and " + TO2.Tag + " are breaking separation rules";

            using (StreamWriter outputFile = new StreamWriter(@"SeparatationEventLog.txt", true))
            {
                outputFile.WriteLine(output);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ATMRefactored.Interfaces;

namespace ATMRefactored
{
    public class EventRendition : IEventRendition
    {
        
        private int verticalSeparation = 300;
        private int horizontalSeparation = 5000;

        public void RenderEvents(List<TrackObject> objectsToCheck)
        {
            for (int i = 0; i < objectsToCheck.Count - 1; i++)
            {
                for (int j = i + 1; j < objectsToCheck.Count; j++)
                {
                    if (IsInOtherAirSpace(objectsToCheck[i], objectsToCheck[j]))
                    {
                        Console.WriteLine(objectsToCheck[i].Tag + " and " + objectsToCheck[j].Tag + "are breaking separation rules!");
                        LogSeparationEvent(objectsToCheck[i], objectsToCheck[j]);
                    }
                }
            }
        }

        //Will return true if a Track is in another Track's airspace
        public bool IsInOtherAirSpace(TrackObject TO1, TrackObject TO2)
        {
            double horizontalDistance = CalculateDistance2D(TO1.XCoord, TO2.XCoord, TO1.YCoord, TO2.YCoord);
            int verticalDistance = CalculateDistance1D(TO1.Altitude, TO2.Altitude);

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

        public int CalculateDistance1D(int x1, int x2)
        {
            return Math.Abs(x1 - x2);
        }

        //Returns two dimensional distance
        public double CalculateDistance2D(int x1, int x2, int y1, int y2)
        {
            Int64 xDist = CalculateDistance1D(x1, x2);
            Int64 yDist = CalculateDistance1D(y1, y2);

            return Math.Sqrt((xDist * xDist) + (yDist * yDist));
        }
    }
}

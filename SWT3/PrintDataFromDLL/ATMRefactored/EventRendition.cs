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
        private TupleList<TrackObject, TrackObject> _conflictList = new TupleList<TrackObject, TrackObject>();
        private TupleList<TrackObject, TrackObject> _newObjects = new TupleList<TrackObject, TrackObject>();
        private TupleList<TrackObject, TrackObject> _oldObjects = new TupleList<TrackObject, TrackObject>();
        public void RenderEvents(List<TrackObject> objectsToCheck)
        {
            for (int i = 0; i < objectsToCheck.Count - 1; i++)
            {
                for (int j = i + 1; j < objectsToCheck.Count; j++)
                {
                    if (IsInOtherAirSpace(objectsToCheck[i], objectsToCheck[j]))
                    {
                        _conflictList.Add(objectsToCheck[i], objectsToCheck[j]);
                        LogSeparationEvent(_conflictList);
                        _conflictList.Clear();
                        _conflictList.TrimExcess();
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

        public void LogSeparationEvent(TupleList<TrackObject, TrackObject> TOList)
        {


            foreach (var conflictingObject in TOList)
            {
                _newObjects.Add(conflictingObject.Item1, conflictingObject.Item2);
            }

            foreach (var newEventObject in _newObjects)
            {
                foreach (var oldEventObject in _oldObjects)
                {
                    if (newEventObject.Item1.Tag != oldEventObject.Item1.Tag &&
                        newEventObject.Item2.Tag != oldEventObject.Item2.Tag)
                    {
                        string output = "Timestamp: " + newEventObject.Item1.Timestamp + "\t" +
                                        newEventObject.Item1.Tag + " and " + newEventObject.Item2.Tag + " are breaking separation rules";

                        Console.WriteLine(output);

                        using (StreamWriter outputFile = new StreamWriter(@"SeparatationEventLog.txt", true))
                        {
                            outputFile.WriteLine(output);
                        }

                        _oldObjects.Add(newEventObject.Item1, newEventObject.Item2);
                       
                    }
                    
                }
            }

            foreach (var oldEventObject in _oldObjects)
            {
                foreach (var newEventObject in _newObjects)
                {
                    if (newEventObject.Item1.Tag == oldEventObject.Item1.Tag &&
                        newEventObject.Item2.Tag == oldEventObject.Item2.Tag)
                    {
                        string output = "Timestamp: " + newEventObject.Item1.Timestamp + "\t" +
                                        newEventObject.Item1.Tag + " and " + newEventObject.Item2.Tag + " has stopped breaking seperation rules";

                        Console.WriteLine(output);

                        using (StreamWriter outputFile = new StreamWriter(@"SeparatationEventLog.txt", true))
                        {
                            outputFile.WriteLine(output);
                        }

                        _oldObjects.Remove(newEventObject);
                        _oldObjects.TrimExcess();

                    }

                }
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

    public class TupleList<T1, T2> : List<Tuple<T1, T2>>
    {
        public void Add(T1 item1, T2 item2)
        {
            Add(new Tuple<T1, T2>(item1, item2));
        }
    }
}

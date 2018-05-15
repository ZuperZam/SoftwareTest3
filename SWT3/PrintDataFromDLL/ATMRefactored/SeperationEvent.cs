using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ATMRefactored.Interfaces;

namespace ATMRefactored
{
    public class SeperationEvent : ISeperationEvent
    {
        private int verticalSeparation = 300;
        private int horizontalSeparation = 5000;
        public TupleList<TrackObject, TrackObject> _conflictList { get; set; }
        public TupleList<TrackObject, TrackObject> _oldObjects { get; set; }
        public ILogWriter _LogWriter;
        public IEventRendition _eventRendition;
        

        public SeperationEvent(ILogWriter logWriter, IEventRendition eventRendition)
        {
            _conflictList = new TupleList<TrackObject, TrackObject>();
            _oldObjects = new TupleList<TrackObject, TrackObject>();
            _LogWriter = logWriter;
            _eventRendition = eventRendition;
        }
        
        public void CheckEvents(List<TrackObject> objectsToCheck)
        {
            for (int i = 0; i < objectsToCheck.Count - 1; i++)
            {
                for (int j = i + 1; j < objectsToCheck.Count; j++)
                {
                    if (IsInOtherAirSpace(objectsToCheck[i], objectsToCheck[j]))
                    {
                        _conflictList.Add(objectsToCheck[i], objectsToCheck[j]);

                        string output = objectsToCheck[i].Tag + " and " + objectsToCheck[j].Tag + " are breaking separation rules";

                        _eventRendition.RenderEvent(output);
                    }
                }
            }

            LogSeparationEvent(_conflictList);
            _conflictList.Clear();
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
            bool isInList = false;
            bool isNotInList = true;
            bool hasChanged = false;

            foreach (var newEventObject in TOList)
            {
                foreach (var oldEventObject in _oldObjects)
                {
                    if ((Equals(newEventObject.Item1.Tag, oldEventObject.Item1.Tag) && Equals(newEventObject.Item2.Tag, oldEventObject.Item2.Tag)) || 
                        (Equals(newEventObject.Item1.Tag, oldEventObject.Item2.Tag) && Equals(newEventObject.Item2.Tag, oldEventObject.Item1.Tag)))
                    {
                        isInList = true;
                    }
                }

                if (!isInList)
                {
                    string output = "Timestamp: " + newEventObject.Item1.Timestamp + "\t" +
                                    newEventObject.Item1.Tag + " and " + newEventObject.Item2.Tag + " are breaking separation rules";

                    _LogWriter.LogEvent(output);

                    hasChanged = true;
                }

                isInList = false;
            }

            foreach (var oldEventObject in _oldObjects)
            {
                foreach (var newEventObject in TOList)
                {
                    if ((Equals(newEventObject.Item1.Tag, oldEventObject.Item1.Tag) && Equals(newEventObject.Item2.Tag, oldEventObject.Item2.Tag)) ||
                        (Equals(newEventObject.Item1.Tag, oldEventObject.Item2.Tag) && Equals(newEventObject.Item2.Tag, oldEventObject.Item1.Tag)))
                    {
                        isNotInList = false;
                    }
                }

                if (isNotInList)
                {
                    string output = "Timestamp: " + oldEventObject.Item1.Timestamp + "\t" +
                                    oldEventObject.Item1.Tag + " and " + oldEventObject.Item2.Tag + " have stopped breaking seperation rules";

                    _LogWriter.LogEvent(output);

                    hasChanged = true;
                }

                isNotInList = true;
            }

            if (hasChanged)
            {
                _oldObjects.Clear();
                //_oldObjects.TrimExcess();

                foreach (var trackObject in TOList)
                {
                    _oldObjects.Add(trackObject);
                }
                hasChanged = false;
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

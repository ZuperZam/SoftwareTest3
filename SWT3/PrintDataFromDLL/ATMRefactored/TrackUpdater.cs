using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMRefactored;
using ATMRefactored.Interfaces;
using Microsoft.Win32;

namespace ATMRefactored
{
    public class TrackUpdater : ITrackUpdater
    {
        public List<TrackObject> _oldTrackObjects { get; set; }
        

        private EventRendition eventRendition = new EventRendition();
        private TrackRendition trackRendition = new TrackRendition();

        public TrackUpdater()
        {
            _oldTrackObjects = new List<TrackObject>();
           
        }

        public void UpdateTracks(List<TrackObject> newTrackObjects)
        {

            foreach (var newTrackObject in newTrackObjects)
            {
                foreach (var oldTrackObject in _oldTrackObjects)
                {
                    if (newTrackObject.Tag != oldTrackObject.Tag)
                    {
                        newTrackObject.Velocity = CalculateVelocity(oldTrackObject, newTrackObject);
                        newTrackObject.Course = CalculateCourse(oldTrackObject, newTrackObject);
                        break;
                    }
                }
            }

            _oldTrackObjects.Clear();

            foreach (var newTrackObject in newTrackObjects)
            {
                _oldTrackObjects.Add(newTrackObject);
            }
            Console.Clear();
            trackRendition.RenderTrack(_oldTrackObjects);
            //eventRendition.RenderEvents(_oldTrackObjects);

        }

        public int CalculateCourse(TrackObject oldTO, TrackObject newTO)
        {
            // angle in degrees
            var angleDeg = Math.Atan2(-(newTO.YCoord - oldTO.YCoord), newTO.XCoord - oldTO.XCoord) * 180 / Math.PI;

            angleDeg += 90;

            if (angleDeg < 0)
                angleDeg += 360;

            return (int)angleDeg;
        }

        //Returns velocity in whole meters per second
        public int CalculateVelocity(TrackObject oldTO, TrackObject newTO)
        {
            TimeSpan timeDiff = newTO.Timestamp - oldTO.Timestamp;
            double dist = this.CalculateDistance2D(oldTO.XCoord, newTO.XCoord, oldTO.YCoord, newTO.YCoord);

            return (int)(dist / (timeDiff.TotalMilliseconds / 1000));    //This will give dist m / timeDiff s
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

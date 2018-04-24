using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMClasses.Interfaces;
using Microsoft.Win32;

namespace ATMClasses
{
    public class TrackUpdater : ITrackUpdater
    {
        public IVelocityCourseCalculator _velocityCourseCalculator { get; }
        private List<TrackObject> _trackObjects;

        public TrackUpdater(IVelocityCourseCalculator velocityCourseCalculator)
        {
            _velocityCourseCalculator = velocityCourseCalculator;
        }

        public List<TrackObject> updateTracks(List<TrackObject> newTrackObjects, List<TrackObject> oldTrackObjects)
        {
            _trackObjects = newTrackObjects;

            foreach (var newTrackObject in _trackObjects)
            {
                foreach (var oldTrackObject in oldTrackObjects)
                {
                    if (newTrackObject.Tag == oldTrackObject.Tag)
                    {
                        newTrackObject.Velocity = _velocityCourseCalculator.CalculateVelocity(oldTrackObject, newTrackObject);
                        newTrackObject.Course = _velocityCourseCalculator.CalculateCourse(oldTrackObject, newTrackObject);
                        break;
                    }
                }
            }

            return _trackObjects;
        }
    }
}

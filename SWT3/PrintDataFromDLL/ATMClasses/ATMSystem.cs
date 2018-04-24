using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMClasses.Interfaces;

namespace ATMClasses
{
    public class ATMSystem
    {
        private List<TrackObject> _oldTrackObjects = new List<TrackObject>();
        private List<TrackObject> _newTrackObjects = new List<TrackObject>();

        private ITrackUpdater _trackUpdater;
        private IVelocityCourseCalculator _velocityCourseCalculator;

        public ATMSystem(
            ITrackListEvent trackListEvent,
            ITrackUpdater trackUpdater,
            IVelocityCourseCalculator velocityCourseCalculator)
        {
            trackListEvent.TrackListReady += OnTrackListReady;

            _trackUpdater = trackUpdater;
            _velocityCourseCalculator = velocityCourseCalculator;
        }

        private void OnTrackListReady(object sender, TrackListEventArgs e)
        {
            _newTrackObjects = e.TrackObjects;
            _trackUpdater.updateTracks(ref _newTrackObjects, _oldTrackObjects);

            _oldTrackObjects.Clear();

            foreach (var newTrackObject in _newTrackObjects)
            {
                _oldTrackObjects.Add(newTrackObject);
            }

            Console.Clear();

            foreach (var oldTrackObject in _oldTrackObjects)
            {
                Console.WriteLine(oldTrackObject.ToString());
            }
        }
    }
}

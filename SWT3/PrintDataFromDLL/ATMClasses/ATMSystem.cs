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
        private ISeparationChecker _separationChecker;

        public ATMSystem(
            ITrackListEvent trackListEvent,
            ITrackUpdater trackUpdater,
            IVelocityCourseCalculator velocityCourseCalculator,
            ISeparationChecker separationChecker)
        {
            trackListEvent.TrackListReady += OnTrackListReady;

            _trackUpdater = trackUpdater;
            _velocityCourseCalculator = velocityCourseCalculator;
            _separationChecker = separationChecker;
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

            for (int i = 0; i < _oldTrackObjects.Count - 1; i++)
            {
                for (int j = i+1; j < _oldTrackObjects.Count; j++)
                {
                    if (_separationChecker.IsInOtherAirSpace(_oldTrackObjects[i], _oldTrackObjects[j]))
                    {
                        Console.WriteLine();
                        Console.WriteLine(_oldTrackObjects[i].Tag + " Colliding with: " + _oldTrackObjects[j].Tag);
                    }
                }
            }
        }
    }
}

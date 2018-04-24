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
        private IPrint _print;


        public ATMSystem(
            ITrackListEvent trackListEvent,
            ITrackUpdater trackUpdater,
            IVelocityCourseCalculator velocityCourseCalculator,
            ISeparationChecker separationChecker,
            IPrint print)
        {
            trackListEvent.TrackListReady += OnTrackListReady;

            _trackUpdater = trackUpdater;
            _velocityCourseCalculator = velocityCourseCalculator;
            _separationChecker = separationChecker;
            _print = print;
        }

        public void OnTrackListReady(object sender, TrackListEventArgs e)
        {
            _newTrackObjects = e.TrackObjects;
            _newTrackObjects = _trackUpdater.updateTracks(_newTrackObjects, _oldTrackObjects);

            _oldTrackObjects.Clear();

            foreach (var newTrackObject in _newTrackObjects)
            {
                _oldTrackObjects.Add(newTrackObject);
            }

            Console.Clear();

            foreach (var oldTrackObject in _oldTrackObjects)
            {
                _print.PrintString(oldTrackObject.ToString());
            }

            for (int i = 0; i < _oldTrackObjects.Count - 1; i++)
            {
                for (int j = i+1; j < _oldTrackObjects.Count; j++)
                {
                    if (_separationChecker.IsInOtherAirSpace(_oldTrackObjects[i], _oldTrackObjects[j]))
                    {
                        _print.PrintString(_oldTrackObjects[i].Tag + " and " + _oldTrackObjects[j].Tag + "are breaking separation rules!");
                        _separationChecker.LogSeparationEvent(_oldTrackObjects[i], _oldTrackObjects[j]);
                    }
                }
            }
        }
    }
}

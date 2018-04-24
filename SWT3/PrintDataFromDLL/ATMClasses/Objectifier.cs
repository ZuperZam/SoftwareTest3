using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMClasses.Interfaces;
using TransponderReceiver;

namespace ATMClasses
{
    public class Objectifier : ITrackListEvent, IObjectifier
    {
        public event EventHandler<TrackListEventArgs> TrackListReady;

        private ITrackingValidation _trackingValidation;
        private ITransponderParsing _transponderParsing;
        private IDateFormatter _dateFormatter;

        private List<TrackObject> _trackObjects = new List<TrackObject>();

        public Objectifier( 
            ITransponderReceiver receiver,
            ITrackingValidation trackingValidation,
            ITransponderParsing transponderParsing,
            IDateFormatter dateFormatter)
        {
            receiver.TransponderDataReady += MakeTrack;

            _trackingValidation = trackingValidation;
            _transponderParsing = transponderParsing;
            _dateFormatter = dateFormatter;
        }

        public void MakeTrack(object sender, RawTransponderDataEventArgs e)
        {
            _trackObjects.Clear();
            foreach (var data in e.TransponderData) //foreach string in the stringlist
            {
                var trackData = _transponderParsing.TransponderParser(data); //Parse string (contains all track data)

                if (_trackingValidation.IsTrackInMonitoredAirspace(trackData))   //Only if plane is inside the Monitored area
                {
                    var track = new TrackObject(trackData); //Make new trackObject
                    track.PrettyTimeStamp = _dateFormatter.FormatTimestamp(trackData[4]);   //Add formated date to the Track object

                    _trackObjects.Add(track);   //Add the track to the list of Tracks
                }
            }

            if (_trackObjects.Count != 0)   //If there are any trackObjects
            {
                var handler = TrackListReady;
                handler?.Invoke(this, new TrackListEventArgs(_trackObjects));   //Invoke TrackListReady event, containing all the trackObjects
            }
        }

    }
}

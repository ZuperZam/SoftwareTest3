using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMRefactored;
using ATMRefactored.Interfaces;
using TransponderReceiver;

namespace ATMRefactored
{
    public class TransponderParsing : ITransponderParsing
    {
        public TrackingFiltering trackingFiltering;
        private List<TrackObject> _trackObjects = new List<TrackObject>();

        public TransponderParsing(ITransponderReceiver receiver)
        {
            trackingFiltering = new TrackingFiltering();
            receiver.TransponderDataReady += MakeTrack;
        }
        public List<string> TransponderParser(string transponderData)
        {
            List<string> transponderParts = transponderData.Split(';').ToList();
            return transponderParts;
        }

        public void MakeTrack(object sender, RawTransponderDataEventArgs e)
        {
            foreach (var data in e.TransponderData) //foreach string in the stringlist
            {
                var trackData = TransponderParser(data);

                var track = new TrackObject(trackData);

                _trackObjects.Add(track);
            }

            trackingFiltering.IsTrackInMonitoredAirspace(_trackObjects);
        }
    }
}

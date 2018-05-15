using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMRefactored;
using ATMRefactored.Interfaces;
using TransponderReceiver;
using System.Globalization;

namespace ATMRefactored
{
    public class TransponderParsing : ITransponderParsing
    {
        
        private ITrackingFiltering _trackingFiltering;


        public List<TrackObject> trackObjects
        {
            get; set;
        }

        public TransponderParsing(ITransponderReceiver receiver, ITrackingFiltering trackingFiltering)
        {
            _trackingFiltering = trackingFiltering;
            trackObjects = new List<TrackObject>();
            receiver.TransponderDataReady += MakeTrack;
        }
        public List<string> TransponderParser(string transponderData)
        {
            List<string> transponderParts = transponderData.Split(';').ToList();
            return transponderParts;
        }

        public void MakeTrack(object sender, RawTransponderDataEventArgs e)
        {
            //var IsInList = false;
            //var trackObjects = new List<TrackObject>();
            //if (trackObjects.Count > 0)
            {
                trackObjects.Clear();
                
            }

            foreach (var data in e.TransponderData) //foreach string in the stringlist
            {
                var trackData = TransponderParser(data);

                var track = new TrackObject(trackData) {PrettyTimeStamp = FormatTimestamp(trackData[4])};

                
                    trackObjects.Add(track);
                
            }

            _trackingFiltering.IsTrackInMonitoredAirspace(trackObjects);
        }

        public string FormatTimestamp(string timestamp)
        {
            string format = "yyyyMMddHHmmssfff";    //Set input format
            DateTime date = DateTime.ParseExact(timestamp, format, CultureInfo.CreateSpecificCulture("en-US"));
            string dateformat = String.Format(new CultureInfo("en-US"), "{0:MMMM d}{1}{0:, yyyy, 'at' HH:mm:ss 'and' fff 'milliseconds'}", date, GetDaySuffix(date));   //Format date correctly

            return dateformat;
        }

        public string GetDaySuffix(DateTime timeStamp)
        {
            //returns "st", "nd", "rd" or "th"
            return (timeStamp.Day % 10 == 1 && timeStamp.Day != 11) ? "st"
                : (timeStamp.Day % 10 == 2 && timeStamp.Day != 12) ? "nd"
                : (timeStamp.Day % 10 == 3 && timeStamp.Day != 13) ? "rd"
                : "th";
        }
    }
}

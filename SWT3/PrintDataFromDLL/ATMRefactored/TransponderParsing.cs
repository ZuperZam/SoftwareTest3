﻿using System;
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
        
        public TrackingFiltering trackingFiltering;
        

        public List<TrackObject> trackObjects
        {
            get; set;

        }

        public TransponderParsing(ITransponderReceiver receiver)
        {
            trackingFiltering = new TrackingFiltering();
            receiver.TransponderDataReady += MakeTrack;
            trackObjects = new List<TrackObject>();
        }
        public List<string> TransponderParser(string transponderData)
        {
            List<string> transponderParts = transponderData.Split(';').ToList();
            return transponderParts;
        }

        public void MakeTrack(object sender, RawTransponderDataEventArgs e)
        {
            var IsInList = false;

            foreach (var data in e.TransponderData) //foreach string in the stringlist
            {
                var trackData = TransponderParser(data);

                var track = new TrackObject(trackData) {PrettyTimeStamp = FormatTimestamp(trackData[4])};

                foreach (var d in trackObjects)
                {
                    if (d.Tag == track.Tag)
                    {
                        IsInList = true;
                        d.Altitude = track.Altitude;
                        d.PrettyTimeStamp = track.PrettyTimeStamp;
                        d.XCoord = track.XCoord;
                        d.YCoord = track.YCoord;
                    }
                }

                if (IsInList == false)
                {
                    trackObjects.Add(track);
                }
                IsInList = false;
            }

            trackingFiltering.IsTrackInMonitoredAirspace(trackObjects);
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

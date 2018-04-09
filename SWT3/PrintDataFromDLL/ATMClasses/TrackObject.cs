using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMClasses
{
    public class TrackObject
    {
        public string Tag { get; set; }
        public string XCoord { get; set; }
        public string YCoord { get; set; }
        public string Altitude { get; set; }
        public string Timestamp { get; set; }

        public TrackObject(List<string> trackInfo)
        {
            Tag = trackInfo[0];
            XCoord = trackInfo[1];
            YCoord = trackInfo[2];
            Altitude = trackInfo[3];
            Timestamp = trackInfo[4];
        }
    }
}

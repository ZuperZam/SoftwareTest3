using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMClasses.Interfaces;

namespace ATMClasses
{
    public class TrackObject
    {
        public string Tag { get; set; }
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public int Altitude { get; set; }
        public int Velocity { get; set; }
        public int Course { get; set; }
        public DateTime Timestamp { get; set; }
        public string PrettyTimeStamp { get; set; }

        public IDateFormatter dateFormatter = new DateFormatter();

        public TrackObject(List<string> trackInfo)
        {
            Tag = trackInfo[0];
            XCoord = int.Parse(trackInfo[1]);
            YCoord = int.Parse(trackInfo[2]);
            Altitude = int.Parse(trackInfo[3]);
            Timestamp = DateTime.ParseExact(trackInfo[4], "yyyyMMddHHmmssfff",
                System.Globalization.CultureInfo.InvariantCulture);
            PrettyTimeStamp = dateFormatter.FormatTimestamp(trackInfo[4]);

            Velocity = 0;
            Course = 0;
        }

        public override string ToString()
        {
            var str = "Tag:\t\t" + Tag + "\n" +
                      "X coordinate:\t" + XCoord + " meters\n" +
                      "Y coordinate:\t" + YCoord + " meters\n" +
                      "Altitude:\t" + Altitude + " meters\n" +
                      "Timestamp:\t" + PrettyTimeStamp + "\n" +
                      "Velocity:\t" + Velocity + " m/s\n" +
                      "Course:\t" + Course + " degrees";
            return str;
        }
    }
}

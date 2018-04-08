using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ATMClasses
{
    class Print
    {
        public static void printTrack(TrackObject track)
        {
            Console.WriteLine("Tag:\t\t" + track.Tag);
            Console.WriteLine("x-coordinate:\t\t" + track.XCoord);
            Console.WriteLine("y-coordinate:\t\t" + track.YCoord);
            Console.WriteLine("altitude:\t\t" + track.Altitude);
            Console.WriteLine("timestamp:\t\t" + track.Timestamp);
        }
    }
}

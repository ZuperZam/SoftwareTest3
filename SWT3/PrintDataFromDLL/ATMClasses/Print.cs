using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ATMClasses
{
    public class Print
    {
        public static void printTrack(TrackObject track)
        {
            Console.WriteLine("Tag:\t\t" + track.Tag);
            Console.WriteLine("x-coordinate:\t" + track.XCoord + " meters");
            Console.WriteLine("y-coordinate:\t" + track.YCoord + " meters");
            Console.WriteLine("altitude:\t" + track.Altitude + " meters");
            Console.WriteLine("timestamp:\t" + track.Timestamp);
            Console.WriteLine();
        }
    }
}

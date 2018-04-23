using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ATMClasses.Interfaces;

namespace ATMClasses
{
    public class Print : IPrint
    {
        public void printTrack(TrackObject track)
        {
            Console.WriteLine(track.ToString());
            Console.WriteLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMRefactored.Interfaces;

namespace ATMRefactored
{
    public class TrackRendition : ITrackRendition
    {
        public void RenderTrack(List<TrackObject> objectsToPrint)
        {
            foreach (var data in objectsToPrint)
            {
                Console.WriteLine(data.ToString());
                Console.WriteLine();
            }
        }
    }
}

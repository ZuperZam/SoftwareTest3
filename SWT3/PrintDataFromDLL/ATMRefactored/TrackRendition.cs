using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMRefactored
{
    public class TrackRendition
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

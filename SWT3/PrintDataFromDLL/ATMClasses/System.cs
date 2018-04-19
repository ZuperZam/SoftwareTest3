using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMClasses
{
    public class System
    {
        private List<TrackObject> _trackList;

        public System()
        { }

        public void SeparationEvent(List<TrackObject> currTrackList)
        {
            foreach(var curr in currTrackList)
            {
                foreach(var prev in _trackList)
                {
                    if ()
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMClasses.Interfaces;

namespace ATMClasses
{
    class ATMSystem
    {
        public ATMSystem(
            ITrackListEvent trackListEvent)
        {
            trackListEvent.TrackListReady += OnTrackListReady;

        }

        private void OnTrackListReady(object sender, TrackListEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

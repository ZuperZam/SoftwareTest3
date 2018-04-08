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
        public string HVelocity { get; set; }
        public string Direction { get; set; }
    }
}

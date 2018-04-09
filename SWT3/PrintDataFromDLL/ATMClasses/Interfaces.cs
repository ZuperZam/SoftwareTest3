using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMClasses
{
    interface IPrint
    {
        void printTrack(TrackObject track);
    }

    interface ITextFormatter
    {
        void FormatText(string x);
    }
}
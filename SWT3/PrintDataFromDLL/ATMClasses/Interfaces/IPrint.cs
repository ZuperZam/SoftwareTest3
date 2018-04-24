﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMClasses.Interfaces
{
    public interface IPrint
    {
        void PrintTrack(TrackObject track);
        void PrintString(string outputString);
    }
}

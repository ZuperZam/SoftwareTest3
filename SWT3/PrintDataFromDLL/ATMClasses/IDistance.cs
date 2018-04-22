using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMClasses
{
    interface IDistance
    {
        int CalculateDistance1D(int x1, int x2);
        double CalculateDistance2D(int x1, int x2, int y1, int y2);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMClasses.Interfaces
{
    public interface IVelocityCourseCalculator
    {
       
        //Returns course in whole degrees. North is 0
        int CalculateCourse(TrackObject oldTO, TrackObject newTO);
        

        //Returns velocity in whole meters per second
        int CalculateVelocity(TrackObject oldTO, TrackObject newTO);

    }
}

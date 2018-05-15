using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMRefactored.Interfaces;

namespace ATMRefactored
{
    public class EventRendition : IEventRendition
    {
        public void RenderEvent(string eventString)
        {
            Console.WriteLine(eventString);
        }
    }
}

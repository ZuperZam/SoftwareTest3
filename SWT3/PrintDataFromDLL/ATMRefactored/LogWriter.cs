using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMRefactored.Interfaces;

namespace ATMRefactored
{
    public class LogWriter : ILogWriter
    {
        public void LogEvent(string eventMessage)
        {
            using (StreamWriter outputFile = new StreamWriter(@"SeparatationEventLog.txt", true))
            {
                outputFile.WriteLine(eventMessage);
            }
        }
    }
}

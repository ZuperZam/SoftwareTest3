using System;
using System.Collections.Generic;
using TransponderReceiver;

namespace ATMRefactored.Interfaces
{
    public interface ITransponderParsing
    {
        List<string> TransponderParser(string transponderData);
        void MakeTrack(object sender, RawTransponderDataEventArgs e);
        string FormatTimestamp(string timestamp);

        string GetDaySuffix(DateTime timeStamp);
    }
}

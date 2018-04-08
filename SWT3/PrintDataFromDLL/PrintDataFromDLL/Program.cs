using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMClasses;
using TransponderReceiver;

namespace PrintDataFromDLL
{
    class Program
    {
        static void Main(string[] args)
        {
            var transponderDataReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            transponderDataReceiver.TransponderDataReady += new EventHandler<RawTransponderDataEventArgs>(OnTransponderDataReady);

            for (;;)
            {
            }
        }

        public static void OnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
           // Console.WriteLine(e.TransponderData.Count);
            foreach (var data in e.TransponderData)
            {
                var words = TransponderParsing.TransponderParser(data);

                Console.WriteLine("tag:\t\t\t" + words[0]);
                Console.WriteLine("x-coordinate:\t\t" + words[1]);
                Console.WriteLine("y-coordinate:\t\t" + words[2]);
                Console.WriteLine("altitude:\t\t" + words[3]);
                Console.WriteLine("timestamp:\t\t" + words[4]);
                Console.WriteLine();
                Console.WriteLine(TrackingValidation.IsTrackInMonitoredAirspace(words));
                Console.WriteLine(DateFormatter.FormatTimestamp(words[4]));
                
            }
        }
    }
}

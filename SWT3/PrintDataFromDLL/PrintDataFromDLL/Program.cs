using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ATMClasses;
using ATMClasses.Interfaces;
using TransponderReceiver;

namespace PrintDataFromDLL
{
    class Program
    {
        static void Main(string[] args)
        {
            var transponderDataReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            transponderDataReceiver.TransponderDataReady += OnTransponderDataReady;

            for (;;)
            {
            }
        }

        public static void OnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            ITransponderParsing TP = new TransponderParsing();
            ITrackingValidation TV = new TrackingValidation();
            IPrint P = new Print();
            Console.Clear();    //Clear old tracks
            
            foreach (var data in e.TransponderData) //foreach string in the stringlist
            {
                var words = TP.TransponderParser(data); //Parse string (contains all track data)

                //words[4] = DateFormatter.FormatTimestamp(words[4]); //Replace timestamp with better formatted date

                if (TV.IsTrackInMonitoredAirspace(words))   //Only if plane is inside the Monitored area
                {
                    var track = new TrackObject(words); //Make new trackObject
                    P.printTrack(track);    //print track data
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;
using ATMRefactored;
using ATMRefactored.Interfaces;

namespace ATMRefactored
{
    public class Program
    {
        static void Main(string[] args)
        {
            var transponderDataReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            transponderDataReceiver.TransponderDataReady += OnTransponderDataReady;

            var eventRendition = new EventRendition();
            var logWriter = new LogWriter();
            var seperationEvent = new SeperationEvent(logWriter, eventRendition);
            var trackRendition = new TrackRendition();
            var trackUpdater = new TrackUpdater(seperationEvent, trackRendition);
            var trackingFiltering = new TrackingFiltering(trackUpdater);
            
            var transponderParsing = new TransponderParsing(transponderDataReceiver, trackingFiltering);
            

            Console.ReadLine();
        }

        public static void OnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {

        }
    }
}

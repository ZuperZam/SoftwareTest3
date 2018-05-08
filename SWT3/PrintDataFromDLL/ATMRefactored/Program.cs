using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;
using ATMRefactored;
using ATMRefactored.Interfaces;
using TransponderReceiver;
namespace ATMRefactored
{
    public class Program
    {
        static void Main(string[] args)
        {
            var transponderDataReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            transponderDataReceiver.TransponderDataReady += OnTransponderDataReady;
            var transponderParsing = new TransponderParsing(transponderDataReceiver);
            var trackingFiltering = new TrackingFiltering();
            var trackUpdater = new TrackUpdater();
            var trackRendition = new TrackRendition();
            var eventRendition = new EventRendition();

            Console.ReadLine();
        }

        public static void OnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {

        }
    }
}

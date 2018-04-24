using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
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

            var trackingValidation = new TrackingValidation();
            var transponderParsing = new TransponderParsing();
            var dateFormatter = new DateFormatter();

            var distance = new Distance();
            var velocityCourseCalculator =  new VelocityCourseCalculater(distance);
            var trackUpdater = new TrackUpdater(velocityCourseCalculator);
            var separationChecker = new SeparationChecker(distance);
            var print = new Print();

            ITrackListEvent objectifier = new Objectifier(transponderDataReceiver, trackingValidation, transponderParsing, dateFormatter);

            var atmSystem = new ATMSystem(objectifier, trackUpdater, velocityCourseCalculator, separationChecker, print);

            System.Console.ReadLine();
        }

        public static void OnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
           
        }
    }
}

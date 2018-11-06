using ATM.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATM.EventArgsClasses;

namespace ATM.classes
{
    public class ConsoleOutPutter : IConsoleOutPutter
    {

        public static int ticks = 0;
        public IAnalyser _Analyser { get; set; }


        public ConsoleOutPutter(IAnalyser analyser)
        {
            _Analyser = analyser;

            _Analyser.AnalysedDataReadyEvent += OutPutAircraftDataEventHandler;          


        }
        
        public void ClearConsole()
        {
            Console.Clear();
        }


        public void OutPutAircraftDataEventHandler(object o, AnalysedTransponderDataEventArgs e)
        {
            OutPutAircraftsWithinArea(e._AircraftData);
            
        }

        public void OutPutAircraftsWithinArea(List<AircraftData> aircraftData)
        {

            ClearConsole();
            Console.WriteLine($"Received transponder data, count : {ticks}");

            foreach (var item in aircraftData)
            {
                Console.WriteLine(item.ToString());
            }

            ticks++;
        }

     
    }
}
